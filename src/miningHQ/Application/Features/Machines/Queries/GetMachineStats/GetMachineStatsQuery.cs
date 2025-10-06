using Application.Features.Machines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Machines.Queries.GetMachineStats;

public class GetMachineStatsQuery : IRequest<GetMachineStatsResponse>, ICachableRequest
{
    public Guid MachineId { get; set; }
    
    public string CacheKey => $"GetMachineStats({MachineId})";
    public bool BypassCache { get; set; } = true;
    public string? CacheGroupKey => "GetMachines";
    public TimeSpan? SlidingExpiration { get; set; }

    public class GetMachineStatsQueryHandler : IRequestHandler<GetMachineStatsQuery, GetMachineStatsResponse>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;
        private readonly MachineBusinessRules _machineBusinessRules;

        public GetMachineStatsQueryHandler(
            IMachineRepository machineRepository,
            IDailyWorkDataRepository dailyWorkDataRepository,
            IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository,
            IMaintenanceRepository maintenanceRepository,
            IMapper mapper,
            MachineBusinessRules machineBusinessRules)
        {
            _machineRepository = machineRepository;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _maintenanceRepository = maintenanceRepository;
            _mapper = mapper;
            _machineBusinessRules = machineBusinessRules;
        }

        public async Task<GetMachineStatsResponse> Handle(GetMachineStatsQuery request, CancellationToken cancellationToken)
        {
            // Check if machine exists
            var machine = await _machineRepository.GetAsync(
                predicate: m => m.Id == request.MachineId,
                cancellationToken: cancellationToken
            );
            
            await _machineBusinessRules.MachineShouldExistWhenSelected(machine);

            // Get all daily work data for this machine
            var workDataList = await _dailyWorkDataRepository.GetListAsync(
                predicate: dwd => dwd.MachineId == request.MachineId,
                cancellationToken: cancellationToken
            );

            // Get all fuel consumption data for this machine
            var fuelDataList = await _dailyFuelConsumptionDataRepository.GetListAsync(
                predicate: dfc => dfc.MachineId == request.MachineId,
                cancellationToken: cancellationToken
            );

            // Get all maintenance records for this machine
            var maintenanceList = await _maintenanceRepository.GetListAsync(
                predicate: m => m.MachineId == request.MachineId,
                orderBy: q => q.OrderByDescending(m => m.MaintenanceDate),
                cancellationToken: cancellationToken
            );

            // Calculate statistics
            var totalWorkDays = workDataList.Items.Count;
            var accumulatedWorkHours = (decimal)workDataList.Items.Sum(w => w.WorkingHoursOrKm);
            var initialHours = (decimal)(machine.InitialWorkingHoursOrKm ?? 0);
            var totalWorkHours = initialHours + accumulatedWorkHours;
            var totalFuelUsed = (decimal)fuelDataList.Items.Sum(f => f.FuelConsumption);
            var averageFuelPerHour = totalWorkHours > 0 ? totalFuelUsed / totalWorkHours : 0m;
            var maintenanceCount = maintenanceList.Items.Count;
            var lastMaintenanceDate = maintenanceList.Items.FirstOrDefault()?.MaintenanceDate;
            var totalProduction = 0m; // ProductionAmount property doesn't exist in DailyWorkData entity

            // Calculate next scheduled maintenance (estimate: every 500 hours or 3 months)
            DateTime? nextScheduledMaintenance = null;
            if (lastMaintenanceDate.HasValue)
            {
                nextScheduledMaintenance = lastMaintenanceDate.Value.AddMonths(3);
            }
            // Or based on working hours
            else if (totalWorkHours > 0)
            {
                // Next maintenance at next 500-hour interval
                var nextMaintenanceHour = Math.Ceiling(totalWorkHours / 500) * 500;
                var hoursUntilMaintenance = nextMaintenanceHour - totalWorkHours;
                // Estimate date based on average usage (assume 8 hours/day)
                nextScheduledMaintenance = DateTime.Now.AddDays((double)(hoursUntilMaintenance / 8));
            }

            var response = new GetMachineStatsResponse
            {
                MachineId = machine.Id,
                MachineName = machine.Name,
                TotalWorkDays = totalWorkDays,
                TotalWorkHours = totalWorkHours,
                TotalFuelUsed = totalFuelUsed,
                AverageFuelConsumptionPerHour = Math.Round(averageFuelPerHour, 2),
                MaintenanceCount = maintenanceCount,
                LastMaintenanceDate = lastMaintenanceDate,
                NextScheduledMaintenance = nextScheduledMaintenance,
                TotalProductionAmount = totalProduction
            };

            return response;
        }
    }
}
