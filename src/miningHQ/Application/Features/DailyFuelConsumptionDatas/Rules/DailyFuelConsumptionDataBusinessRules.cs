using Application.Features.DailyFuelConsumptionDatas.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.DailyFuelConsumptionDatas.Rules;

public class DailyFuelConsumptionDataBusinessRules : BaseBusinessRules
{
    private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;

    public DailyFuelConsumptionDataBusinessRules(IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository)
    {
        _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
    }

    public Task DailyFuelConsumptionDataShouldExistWhenSelected(DailyFuelConsumptionData? dailyFuelConsumptionData)
    {
        if (dailyFuelConsumptionData == null)
            throw new BusinessException(DailyFuelConsumptionDatasBusinessMessages.DailyFuelConsumptionDataNotExists);
        return Task.CompletedTask;
    }

    public async Task DailyFuelConsumptionDataIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        DailyFuelConsumptionData? dailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.GetAsync(
            predicate: dfcd => dfcd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DailyFuelConsumptionDataShouldExistWhenSelected(dailyFuelConsumptionData);
    }
}