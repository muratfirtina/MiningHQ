using Application.Features.Departments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Departments.Rules;

public class DepartmentBusinessRules : BaseBusinessRules
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentBusinessRules(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task DepartmentShouldExistWhenSelected(Department? department)
    {
        if (department == null)
            throw new BusinessException(DepartmentsBusinessMessages.DepartmentNotExists);
        return Task.CompletedTask;
    }

    public async Task DepartmentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Department? department = await _departmentRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DepartmentShouldExistWhenSelected(department);
    }
}