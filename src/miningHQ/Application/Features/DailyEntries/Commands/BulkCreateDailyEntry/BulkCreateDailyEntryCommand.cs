using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.DailyEntries.Commands.BulkCreateDailyEntry;

public class BulkCreateDailyEntryCommand : IRequest<BulkCreateDailyEntryResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public DateTime EntryDate { get; set; }
    public List<MachineEntryItem> MachineEntries { get; set; }
    
    public string? CacheKey => "";
    public bool BypassCache => false;
    public string[]? CacheGroupKey => new[] { "GetDailyWorkDatas", "GetDailyFuelConsumptionDatas", "GetMachines" };

    public class BulkCreateDailyEntryCommandHandler : IRequestHandler<BulkCreateDailyEntryCommand, BulkCreateDailyEntryResponse>
    {
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public BulkCreateDailyEntryCommandHandler(
            IDailyWorkDataRepository dailyWorkDataRepository,
            IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository,
            IMachineRepository machineRepository,
            IMapper mapper)
        {
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _machineRepository = machineRepository;
            _mapper = mapper;
        }

        public async Task<BulkCreateDailyEntryResponse> Handle(BulkCreateDailyEntryCommand request, CancellationToken cancellationToken)
        {
            var workDataList = new List<DailyWorkData>();
            var fuelDataList = new List<DailyFuelConsumptionData>();
            var successCount = 0;
            var errorMessages = new List<string>();

            foreach (var entry in request.MachineEntries)
            {
                try
                {
                    // Validate machine exists
                    var machine = await _machineRepository.GetAsync(m => m.Id == entry.MachineId, cancellationToken: cancellationToken);
                    if (machine == null)
                    {
                        errorMessages.Add($"Makina bulunamadı: {entry.MachineId}");
                        continue;
                    }

                    // Calculate working hours difference
                    var currentTotalHours = entry.CurrentTotalHours;
                    var newTotalHours = entry.NewTotalHours;
                    var workingHours = newTotalHours - currentTotalHours;

                    // Validate working hours
                    if (workingHours < 0)
                    {
                        errorMessages.Add($"{machine.Name}: Yeni saat, mevcut saatten küçük olamaz");
                        continue;
                    }

                    if (workingHours > 24)
                    {
                        errorMessages.Add($"{machine.Name}: Günlük çalışma saati 24 saati geçemez");
                        continue;
                    }

                    // Create DailyWorkData only if there are working hours
                    if (workingHours > 0)
                    {
                        var dailyWorkData = new DailyWorkData
                        {
                            Id = Guid.NewGuid(),
                            Date = request.EntryDate,
                            WorkingHoursOrKm = workingHours,
                            MachineId = entry.MachineId,
                            CreatedDate = DateTime.UtcNow
                        };
                        workDataList.Add(dailyWorkData);
                    }

                    // Create DailyFuelConsumptionData only if there is fuel consumption
                    if (entry.FuelConsumption > 0)
                    {
                        var dailyFuelData = new DailyFuelConsumptionData
                        {
                            Id = Guid.NewGuid(),
                            Date = request.EntryDate,
                            FuelConsumption = entry.FuelConsumption,
                            MachineId = entry.MachineId,
                            CreatedDate = DateTime.UtcNow
                        };
                        fuelDataList.Add(dailyFuelData);
                    }

                    if (workingHours > 0 || entry.FuelConsumption > 0)
                    {
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Hata: {ex.Message}");
                }
            }

            // Bulk insert
            if (workDataList.Any())
            {
                await _dailyWorkDataRepository.AddRangeAsync(workDataList);
            }

            if (fuelDataList.Any())
            {
                await _dailyFuelConsumptionDataRepository.AddRangeAsync(fuelDataList);
            }

            return new BulkCreateDailyEntryResponse
            {
                Success = errorMessages.Count == 0,
                Message = errorMessages.Count == 0 
                    ? $"{successCount} makina için kayıt başarıyla oluşturuldu" 
                    : $"{successCount} başarılı, {errorMessages.Count} hatalı kayıt",
                SuccessCount = successCount,
                ErrorCount = errorMessages.Count,
                ErrorMessages = errorMessages,
                EntryDate = request.EntryDate
            };
        }
    }
}

public class MachineEntryItem
{
    public Guid MachineId { get; set; }
    public int CurrentTotalHours { get; set; }
    public int NewTotalHours { get; set; }
    public double FuelConsumption { get; set; }
}
