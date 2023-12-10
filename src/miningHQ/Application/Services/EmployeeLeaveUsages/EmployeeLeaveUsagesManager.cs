using Application.Features.EmployeeLeaveUsages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EmployeeLeaveUsages;

public class EmployeeLeaveUsagesManager : IEmployeeLeaveUsagesService
{
    private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
    private readonly EmployeeLeaveUsageBusinessRules _employeeLeaveUsageBusinessRules;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeLeaveUsagesManager(IEmployeeLeaveUsageRepository employeeLeaveUsageRepository, EmployeeLeaveUsageBusinessRules employeeLeaveUsageBusinessRules, IEmployeeRepository employeeRepository)
    {
        _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
        _employeeLeaveUsageBusinessRules = employeeLeaveUsageBusinessRules;
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeLeaveUsage?> GetAsync(
        Expression<Func<EmployeeLeaveUsage, bool>> predicate,
        Func<IQueryable<EmployeeLeaveUsage>, IIncludableQueryable<EmployeeLeaveUsage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        EmployeeLeaveUsage? employeeLeave = await _employeeLeaveUsageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return employeeLeave;
    }

    public async Task<IPaginate<EmployeeLeaveUsage>?> GetListAsync(
        Expression<Func<EmployeeLeaveUsage, bool>>? predicate = null,
        Func<IQueryable<EmployeeLeaveUsage>, IOrderedQueryable<EmployeeLeaveUsage>>? orderBy = null,
        Func<IQueryable<EmployeeLeaveUsage>, IIncludableQueryable<EmployeeLeaveUsage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<EmployeeLeaveUsage> employeeLeaveList = await _employeeLeaveUsageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return employeeLeaveList;
    }

    public async Task<EmployeeLeaveUsage> AddAsync(EmployeeLeaveUsage employeeLeaveUsage)
    {
        EmployeeLeaveUsage addedEmployeeLeaveUsage = await _employeeLeaveUsageRepository.AddAsync(employeeLeaveUsage);

        return addedEmployeeLeaveUsage;
    }

    public async Task<EmployeeLeaveUsage> UpdateAsync(EmployeeLeaveUsage employeeLeaveUsage)
    {
        EmployeeLeaveUsage updatedEmployeeLeaveUsage = await _employeeLeaveUsageRepository.UpdateAsync(employeeLeaveUsage);

        return updatedEmployeeLeaveUsage;
    }

    public async Task<EmployeeLeaveUsage> DeleteAsync(EmployeeLeaveUsage employeeLeaveUsage, bool permanent = false)
    {
        EmployeeLeaveUsage deletedEmployeeLeaveUsage = await _employeeLeaveUsageRepository.DeleteAsync(employeeLeaveUsage);

        return deletedEmployeeLeaveUsage;
    }
    
}
