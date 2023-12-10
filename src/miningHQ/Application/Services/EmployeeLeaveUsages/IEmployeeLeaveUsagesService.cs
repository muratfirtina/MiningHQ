using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EmployeeLeaveUsages;

public interface IEmployeeLeaveUsagesService
{
    Task<EmployeeLeaveUsage?> GetAsync(
        Expression<Func<EmployeeLeaveUsage, bool>> predicate,
        Func<IQueryable<EmployeeLeaveUsage>, IIncludableQueryable<EmployeeLeaveUsage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<EmployeeLeaveUsage>?> GetListAsync(
        Expression<Func<EmployeeLeaveUsage, bool>>? predicate = null,
        Func<IQueryable<EmployeeLeaveUsage>, IOrderedQueryable<EmployeeLeaveUsage>>? orderBy = null,
        Func<IQueryable<EmployeeLeaveUsage>, IIncludableQueryable<EmployeeLeaveUsage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<EmployeeLeaveUsage> AddAsync(EmployeeLeaveUsage employeeLeaveUsage);
    Task<EmployeeLeaveUsage> UpdateAsync(EmployeeLeaveUsage employeeLeaveUsage);
    Task<EmployeeLeaveUsage> DeleteAsync(EmployeeLeaveUsage employeeLeaveUsage, bool permanent = false);
}
