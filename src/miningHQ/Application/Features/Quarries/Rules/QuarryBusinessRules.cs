using Application.Features.Quarries.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Quarries.Rules;

public class QuarryBusinessRules : BaseBusinessRules
{
    private readonly IQuarryRepository _quarryRepository;

    public QuarryBusinessRules(IQuarryRepository quarryRepository)
    {
        _quarryRepository = quarryRepository;
    }

    public Task QuarryShouldExistWhenSelected(Quarry? quarry)
    {
        if (quarry == null)
            throw new BusinessException(QuarriesBusinessMessages.QuarryNotExists);
        return Task.CompletedTask;
    }

    public async Task QuarryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Quarry? quarry = await _quarryRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuarryShouldExistWhenSelected(quarry);
    }
}