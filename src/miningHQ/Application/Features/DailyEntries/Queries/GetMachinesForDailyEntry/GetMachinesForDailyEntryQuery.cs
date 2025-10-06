using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DailyEntries.Queries.GetMachinesForDailyEntry;

public class GetMachinesForDailyEntryQuery : IRequest<List<GetMachinesForDailyEntryResponse>>
{
    public class GetMachinesForDailyEntryQueryHandler : IRequestHandler<GetMachinesForDailyEntryQuery, List<GetMachinesForDailyEntryResponse>>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public GetMachinesForDailyEntryQueryHandler(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMachinesForDailyEntryResponse>> Handle(GetMachinesForDailyEntryQuery request, CancellationToken cancellationToken)
        {
            // Get all active machines with their work data
            var machines = await _machineRepository.GetAllAsync(
                include: m => m
                    .Include(x => x.Model)
                        .ThenInclude(model => model.Brand)
                    .Include(x => x.MachineType)
                    .Include(x => x.Quarry)
                    .Include(x => x.CurrentOperator)
                    .Include(x => x.DailyWorkDatas)
                    .Include(x => x.DailyFuelConsumptionDatas),
                cancellationToken: cancellationToken
            );

            var response = new List<GetMachinesForDailyEntryResponse>();

            foreach (var machine in machines)
            {
                // Calculate current total working hours (initial + accumulated)
                var accumulatedWorkingHours = machine.DailyWorkDatas?.Sum(d => d.WorkingHoursOrKm) ?? 0;
                var initialHours = machine.InitialWorkingHoursOrKm ?? 0;
                var totalWorkingHours = initialHours + accumulatedWorkingHours;
                
                // Get last entry date
                var lastWorkDate = machine.DailyWorkDatas?
                    .OrderByDescending(d => d.Date)
                    .FirstOrDefault()?.Date;

                response.Add(new GetMachinesForDailyEntryResponse
                {
                    MachineId = machine.Id,
                    MachineName = machine.Name,
                    BrandName = machine.Model?.Brand?.Name,
                    ModelName = machine.Model?.Name,
                    MachineTypeName = machine.MachineType?.Name,
                    QuarryName = machine.Quarry?.Name,
                    SerialNumber = machine.SerialNumber,
                    CurrentOperatorName = machine.CurrentOperator != null 
                        ? $"{machine.CurrentOperator.FirstName} {machine.CurrentOperator.LastName}" 
                        : null,
                    CurrentTotalHours = totalWorkingHours,
                    LastEntryDate = lastWorkDate,
                    // Default values for entry - NewTotalHours should be empty for user input
                    NewTotalHours = 0,
                    FuelConsumption = 0
                });
            }

            return response.OrderBy(x => x.MachineName).ToList();
        }
    }
}
