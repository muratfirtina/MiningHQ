using Application.Features.EntitledLeaves.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.EntitledLeaves.Rules;

public class EntitledLeaveBusinessRules : BaseBusinessRules
{
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;

    public EntitledLeaveBusinessRules(IEntitledLeaveRepository entitledLeaveRepository)
    {
        _entitledLeaveRepository = entitledLeaveRepository;
    }

    public Task EntitledLeaveShouldExistWhenSelected(EntitledLeave? entitledLeave)
    {
        if (entitledLeave == null)
            throw new BusinessException(EntitledLeavesBusinessMessages.EntitledLeaveNotExists);
        return Task.CompletedTask;
    }

    public async Task EntitledLeaveIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        EntitledLeave? entitledLeave = await _entitledLeaveRepository.GetAsync(
            predicate: el => el.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EntitledLeaveShouldExistWhenSelected(entitledLeave);
    }
    
}