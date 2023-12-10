using Application.Features.EmployeeLeaveUsages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Rules;

public class EmployeeLeaveUsageBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;

    public EmployeeLeaveUsageBusinessRules(IEmployeeLeaveUsageRepository employeeLeaveUsageRepository)
    {
        _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
    }

    public Task EmployeeLeaveShouldExistWhenSelected(EmployeeLeaveUsage? employeeLeaveUsage)
    {
        if (employeeLeaveUsage == null)
            throw new BusinessException(EmployeeLeaveUsagesBusinessMessages.EmployeeLeaveNotExists);
        return Task.CompletedTask;
    }

    public async Task EmployeeLeaveIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        EmployeeLeaveUsage? employeeLeaveUsage = await _employeeLeaveUsageRepository.GetAsync(
            predicate: el => el.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EmployeeLeaveShouldExistWhenSelected(employeeLeaveUsage);
    }
}