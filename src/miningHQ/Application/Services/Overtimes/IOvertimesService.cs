using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Overtimes;

public interface IOvertimesService
{
    Task<Overtime?> GetAsync(
        Expression<Func<Overtime, bool>> predicate,
        Func<IQueryable<Overtime>, IIncludableQueryable<Overtime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Overtime>?> GetListAsync(
        Expression<Func<Overtime, bool>>? predicate = null,
        Func<IQueryable<Overtime>, IOrderedQueryable<Overtime>>? orderBy = null,
        Func<IQueryable<Overtime>, IIncludableQueryable<Overtime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Overtime> AddAsync(Overtime overtime);
    Task<Overtime> UpdateAsync(Overtime overtime);
    Task<Overtime> DeleteAsync(Overtime overtime, bool permanent = false);
}
