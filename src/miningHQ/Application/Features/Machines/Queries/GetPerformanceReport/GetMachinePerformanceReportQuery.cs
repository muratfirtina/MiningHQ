using Application.Features.Machines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Application.Pipelines.Caching;

namespace Application.Features.Machines.Queries.GetPerformanceReport;

public class GetMachinePerformanceReportQuery : IRequest<MachinePerformanceReportDto>, ICachableRequest
{
    public Guid MachineId { get; set; }

    public string CacheKey => $"GetMachinePerformanceReport({MachineId})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetMachines";
    public TimeSpan? SlidingExpiration { get; } = TimeSpan.FromMinutes(5);

    public class GetMachinePerformanceReportQueryHandler
        : IRequestHandler<GetMachinePerformanceReportQuery, MachinePerformanceReportDto>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionRepository;
        private readonly MachineBusinessRules _machineBusinessRules;
        private readonly IMapper _mapper;

        public GetMachinePerformanceReportQueryHandler(
            IMachineRepository machineRepository,
            IDailyWorkDataRepository dailyWorkDataRepository,
            IDailyFuelConsumptionDataRepository dailyFuelConsumptionRepository,
            MachineBusinessRules machineBusinessRules,
            IMapper mapper)
        {
            _machineRepository = machineRepository;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyFuelConsumptionRepository = dailyFuelConsumptionRepository;
            _machineBusinessRules = machineBusinessRules;
            _mapper = mapper;
        }

        public async Task<MachinePerformanceReportDto> Handle(
            GetMachinePerformanceReportQuery request,
            CancellationToken cancellationToken)
        {
            Machine? machine = await _machineRepository.GetAsync(
                predicate: m => m.Id == request.MachineId,
                include: m => m.Include(x => x.Model)
                               .ThenInclude(model => model.Brand)
                               .Include(x => x.MachineType),
                cancellationToken: cancellationToken
            );

            await _machineBusinessRules.MachineShouldExistWhenSelected(machine);

            var now = DateTime.Now;
            
            // Haftalık (Son 7 gün)
            var weekStart = now.AddDays(-7);
            var weeklyData = await GetPeriodPerformance(request.MachineId, weekStart, now, cancellationToken);
            
            // Aylık (Son 30 gün)
            var monthStart = now.AddDays(-30);
            var monthlyData = await GetPeriodPerformance(request.MachineId, monthStart, now, cancellationToken);
            
            // Yıllık (Son 365 gün)
            var yearStart = now.AddDays(-365);
            var yearlyData = await GetPeriodPerformance(request.MachineId, yearStart, now, cancellationToken);

            var report = new MachinePerformanceReportDto
            {
                MachineId = machine.Id,
                MachineName = machine.Name,
                BrandName = machine.Model?.Brand?.Name ?? "",
                ModelName = machine.Model?.Name ?? "",
                WeeklyPerformance = new WeeklyPerformanceDto
                {
                    TotalWorkingHours = weeklyData.TotalHours,
                    TotalWorkingDays = weeklyData.TotalDays,
                    TotalFuelConsumption = weeklyData.TotalFuel,
                    AverageDailyHours = weeklyData.AvgHours,
                    AverageDailyFuelConsumption = weeklyData.AvgFuel,
                    StartDate = weekStart,
                    EndDate = now
                },
                MonthlyPerformance = new MonthlyPerformanceDto
                {
                    TotalWorkingHours = monthlyData.TotalHours,
                    TotalWorkingDays = monthlyData.TotalDays,
                    TotalFuelConsumption = monthlyData.TotalFuel,
                    AverageDailyHours = monthlyData.AvgHours,
                    AverageDailyFuelConsumption = monthlyData.AvgFuel,
                    StartDate = monthStart,
                    EndDate = now
                },
                YearlyPerformance = new YearlyPerformanceDto
                {
                    TotalWorkingHours = yearlyData.TotalHours,
                    TotalWorkingDays = yearlyData.TotalDays,
                    TotalFuelConsumption = yearlyData.TotalFuel,
                    AverageDailyHours = yearlyData.AvgHours,
                    AverageDailyFuelConsumption = yearlyData.AvgFuel,
                    StartDate = yearStart,
                    EndDate = now
                }
            };

            return report;
        }

        private async Task<(double TotalHours, int TotalDays, double TotalFuel, double AvgHours, double AvgFuel)> 
            GetPeriodPerformance(Guid machineId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            // Çalışma verileri
            var workDataList = await _dailyWorkDataRepository.GetListAsync(
                predicate: d => d.MachineId == machineId && d.Date >= startDate && d.Date <= endDate,
                cancellationToken: cancellationToken
            );

            // Yakıt verileri
            var fuelDataList = await _dailyFuelConsumptionRepository.GetListAsync(
                predicate: d => d.MachineId == machineId && d.Date >= startDate && d.Date <= endDate,
                cancellationToken: cancellationToken
            );

            var totalHours = workDataList.Items.Sum(d => d.WorkingHoursOrKm);
            var totalDays = workDataList.Items.Count;
            var totalFuel = fuelDataList.Items.Sum(d => d.FuelConsumption);
            
            var avgHours = totalDays > 0 ? totalHours / totalDays : 0;
            var avgFuel = totalDays > 0 ? totalFuel / totalDays : 0;

            return (totalHours, totalDays, totalFuel, avgHours, avgFuel);
        }
    }
}
