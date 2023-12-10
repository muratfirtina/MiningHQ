using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Employees.Rules;

public class EmployeeBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeBusinessRules(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task EmployeeShouldExistWhenSelected(Employee? employee)
    {
        if (employee == null)
            throw new BusinessException(EmployeesBusinessMessages.EmployeeNotExists);
        return Task.CompletedTask;
    }

    public async Task EmployeeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Employee? employee = await _employeeRepository.GetAsync(
            predicate: e => e.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EmployeeShouldExistWhenSelected(employee);
    }
    public async Task<int> CalculateLeaveDays(Guid id)
    {
        Employee? employee = await _employeeRepository.GetAsync(
            predicate: e => e.Id == id
        );
        DateTime? hireDate = employee?.HireDate;
        if (hireDate != null)
        {
            DateTime anniversaryDate = hireDate.Value.AddYears(DateTime.Now.Year - hireDate.Value.Year);

            int leaveDays = DateTime.Now >= anniversaryDate.AddYears(5) ? 20 : 14;

            return leaveDays;
        }
        else
        {
            return 0;
        }
    }
}