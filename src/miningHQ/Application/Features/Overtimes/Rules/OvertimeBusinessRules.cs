using Application.Features.Overtimes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Overtimes.Rules;

public class OvertimeBusinessRules : BaseBusinessRules
{
    private readonly IOvertimeRepository _overtimeRepository;

    public OvertimeBusinessRules(IOvertimeRepository overtimeRepository)
    {
        _overtimeRepository = overtimeRepository;
    }

    public Task OvertimeShouldExistWhenSelected(Overtime? overtime)
    {
        if (overtime == null)
            throw new BusinessException(OvertimesBusinessMessages.OvertimeNotExists);
        return Task.CompletedTask;
    }

    public async Task OvertimeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Overtime? overtime = await _overtimeRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OvertimeShouldExistWhenSelected(overtime);
    }
}