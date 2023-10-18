using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LeaveUsages;

public interface ILeaveUsagesService
{
    Task<LeaveUsage?> GetAsync(
        Expression<Func<LeaveUsage, bool>> predicate,
        Func<IQueryable<LeaveUsage>, IIncludableQueryable<LeaveUsage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LeaveUsage>?> GetListAsync(
        Expression<Func<LeaveUsage, bool>>? predicate = null,
        Func<IQueryable<LeaveUsage>, IOrderedQueryable<LeaveUsage>>? orderBy = null,
        Func<IQueryable<LeaveUsage>, IIncludableQueryable<LeaveUsage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LeaveUsage> AddAsync(LeaveUsage leaveUsage);
    Task<LeaveUsage> UpdateAsync(LeaveUsage leaveUsage);
    Task<LeaveUsage> DeleteAsync(LeaveUsage leaveUsage, bool permanent = false);
}
