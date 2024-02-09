using Application.Features.EntitledLeaves.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.EntitledLeaves.Rules;

public class EntitledLeaveBusinessRules : BaseBusinessRules
{
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;
    private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;

    public EntitledLeaveBusinessRules(IEntitledLeaveRepository entitledLeaveRepository, IEmployeeLeaveUsageRepository employeeLeaveUsageRepository)
    {
        _entitledLeaveRepository = entitledLeaveRepository;
        _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
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
    
    
    public async Task<int?> CalculateRemainingAnnualLeaves(string? employeeId, CancellationToken cancellationToken)
    {
        var entitledLeaves = await _entitledLeaveRepository.GetAllAsync(
            predicate: el => el.EmployeeId.ToString() == employeeId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        var usedDays = await _employeeLeaveUsageRepository.GetAllAsync(
            predicate: elu => elu.EmployeeId.ToString() == employeeId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        var tolalEntitledLeaves = entitledLeaves.Sum(el => el.EntitledDays);
        var tolalUsedDays = usedDays.Sum(elu => elu.UsedDays);
        
        var remainingAnnualLeaves = tolalEntitledLeaves - tolalUsedDays;
        return remainingAnnualLeaves;
        
    }
    
    
}