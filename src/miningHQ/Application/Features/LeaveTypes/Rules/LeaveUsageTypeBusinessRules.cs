using Application.Features.LeaveTypes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LeaveTypes.Rules;

public class LeaveUsageTypeBusinessRules : BaseBusinessRules
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveUsageTypeBusinessRules(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public Task LeaveUsageShouldExistWhenSelected(LeaveType? leaveUsage)
    {
        if (leaveUsage == null)
            throw new BusinessException(LeaveTypesBusinessMessages.LeaveUsageNotExists);
        return Task.CompletedTask;
    }

    public async Task LeaveUsageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LeaveType? leaveUsage = await _leaveTypeRepository.GetAsync(
            predicate: lu => lu.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LeaveUsageShouldExistWhenSelected(leaveUsage);
    }
}