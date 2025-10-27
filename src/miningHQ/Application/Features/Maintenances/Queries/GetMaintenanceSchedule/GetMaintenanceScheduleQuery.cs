using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Queries.GetMaintenanceSchedule;

public class GetMaintenanceScheduleQuery : IRequest<GetListResponse<GetMaintenanceScheduleDto>>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    
    public string[] Roles => new[] { Admin, Read };

    public class GetMaintenanceScheduleQueryHandler : IRequestHandler<GetMaintenanceScheduleQuery, GetListResponse<GetMaintenanceScheduleDto>>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;

        public GetMaintenanceScheduleQueryHandler(
            IMachineRepository machineRepository, 
            IMaintenanceRepository maintenanceRepository,
            IDailyWorkDataRepository dailyWorkDataRepository)
        {
            _machineRepository = machineRepository;
            _maintenanceRepository = maintenanceRepository;
            _dailyWorkDataRepository = dailyWorkDataRepository;
        }

        public async Task<GetListResponse<GetMaintenanceScheduleDto>> Handle(GetMaintenanceScheduleQuery request, CancellationToken cancellationToken)
        {
            // Tüm makinaları al
            var machines = await _machineRepository.GetListAsync(
                include: m => m
                    .Include(m => m.Model)
                        .ThenInclude(model => model.Brand)
                    .Include(m => m.MachineType)
                    .Include(m => m.Quarry)
                    .Include(m => m.Maintenances.OrderByDescending(mt => mt.MaintenanceDate).Take(1))
                        .ThenInclude(mt => mt.MaintenanceType),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            var scheduleList = new List<GetMaintenanceScheduleDto>();

            foreach (var machine in machines.Items)
            {
                var lastMaintenance = machine.Maintenances?.OrderByDescending(m => m.MaintenanceDate).FirstOrDefault();
                
                // Tüm günlük çalışma verilerini al ve topla (şu anki saat/km)
                var allDailyWorkData = await _dailyWorkDataRepository.GetListAsync(
                    predicate: d => d.MachineId == machine.Id,
                    index: 0,
                    size: int.MaxValue, // Tüm kayıtları al
                    cancellationToken: cancellationToken
                );
                
                // Toplam çalışma saatini hesapla: Başlangıç + Tüm günlük artışlar
                int? currentWorkingHourOrKm = machine.InitialWorkingHoursOrKm;
                if (allDailyWorkData.Items.Any())
                {
                    currentWorkingHourOrKm = (currentWorkingHourOrKm ?? 0) + allDailyWorkData.Items.Sum(d => d.WorkingHoursOrKm);
                }
                int? remainingHourOrKm = null;
                string maintenanceStatus = "Normal";

                if (lastMaintenance?.NextMaintenanceHour.HasValue == true && currentWorkingHourOrKm.HasValue)
                {
                    remainingHourOrKm = lastMaintenance.NextMaintenanceHour.Value - currentWorkingHourOrKm.Value;
                    
                    if (remainingHourOrKm <= 0)
                        maintenanceStatus = "Gecikmiş";
                    else if (remainingHourOrKm <= 50)
                        maintenanceStatus = "Yaklaşıyor";
                }

                scheduleList.Add(new GetMaintenanceScheduleDto
                {
                    MachineId = machine.Id,
                    MachineName = machine.Name ?? "İsimsiz Makine",
                    SerialNumber = machine.SerialNumber,
                    MachineTypeName = machine.MachineType?.Name ?? "-",
                    BrandName = machine.Model?.Brand?.Name ?? "-",
                    ModelName = machine.Model?.Name ?? "-",
                    QuarryName = machine.Quarry?.Name ?? "-",
                    
                    LastMaintenanceDate = lastMaintenance?.MaintenanceDate,
                    LastMaintenanceHourOrKm = lastMaintenance?.MachineWorkingTimeOrKilometer,
                    LastMaintenanceType = lastMaintenance?.MaintenanceType?.Name,
                    LastMaintenanceFirm = lastMaintenance?.MaintenanceFirm,
                    
                    NextMaintenanceHour = lastMaintenance?.NextMaintenanceHour,
                    CurrentWorkingHourOrKm = currentWorkingHourOrKm,
                    RemainingHourOrKm = remainingHourOrKm,
                    MaintenanceStatus = maintenanceStatus
                });
            }

            return new GetListResponse<GetMaintenanceScheduleDto>
            {
                Items = scheduleList,
                Index = machines.Index,
                Size = machines.Size,
                Count = machines.Count,
                Pages = machines.Pages,
                HasPrevious = machines.HasPrevious,
                HasNext = machines.HasNext
            };
        }
    }
}
