using Application.Features.DailyWorkDatas.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.DailyWorkDatas.Rules;

public class DailyWorkDataBusinessRules : BaseBusinessRules
{
    private readonly IDailyWorkDataRepository _dailyWorkDataRepository;

    public DailyWorkDataBusinessRules(IDailyWorkDataRepository dailyWorkDataRepository)
    {
        _dailyWorkDataRepository = dailyWorkDataRepository;
    }

    public Task DailyWorkDataShouldExistWhenSelected(DailyWorkData? dailyWorkData)
    {
        if (dailyWorkData == null)
            throw new BusinessException(DailyWorkDatasBusinessMessages.DailyWorkDataNotExists);
        return Task.CompletedTask;
    }

    public async Task DailyWorkDataIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        DailyWorkData? dailyWorkData = await _dailyWorkDataRepository.GetAsync(
            predicate: dwd => dwd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DailyWorkDataShouldExistWhenSelected(dailyWorkData);
    }
}