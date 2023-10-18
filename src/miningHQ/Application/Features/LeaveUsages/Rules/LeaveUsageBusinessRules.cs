using Application.Features.LeaveUsages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LeaveUsages.Rules;

public class LeaveUsageBusinessRules : BaseBusinessRules
{
    private readonly ILeaveUsageRepository _leaveUsageRepository;

    public LeaveUsageBusinessRules(ILeaveUsageRepository leaveUsageRepository)
    {
        _leaveUsageRepository = leaveUsageRepository;
    }

    public Task LeaveUsageShouldExistWhenSelected(LeaveUsage? leaveUsage)
    {
        if (leaveUsage == null)
            throw new BusinessException(LeaveUsagesBusinessMessages.LeaveUsageNotExists);
        return Task.CompletedTask;
    }

    public async Task LeaveUsageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LeaveUsage? leaveUsage = await _leaveUsageRepository.GetAsync(
            predicate: lu => lu.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LeaveUsageShouldExistWhenSelected(leaveUsage);
    }
}