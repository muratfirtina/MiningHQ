using Application.Features.EmployeeLeaves.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.EmployeeLeaves.Rules;

public class EmployeeLeaveBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;

    public EmployeeLeaveBusinessRules(IEmployeeLeaveRepository employeeLeaveRepository)
    {
        _employeeLeaveRepository = employeeLeaveRepository;
    }

    public Task EmployeeLeaveShouldExistWhenSelected(EmployeeLeave? employeeLeave)
    {
        if (employeeLeave == null)
            throw new BusinessException(EmployeeLeavesBusinessMessages.EmployeeLeaveNotExists);
        return Task.CompletedTask;
    }

    public async Task EmployeeLeaveIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        EmployeeLeave? employeeLeave = await _employeeLeaveRepository.GetAsync(
            predicate: el => el.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EmployeeLeaveShouldExistWhenSelected(employeeLeave);
    }
}