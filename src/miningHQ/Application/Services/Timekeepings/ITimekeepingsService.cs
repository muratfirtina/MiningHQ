using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Timekeepings;

public interface ITimekeepingsService
{
    Task<Timekeeping?> GetAsync(
        Expression<Func<Timekeeping, bool>> predicate,
        Func<IQueryable<Timekeeping>, IIncludableQueryable<Timekeeping, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Timekeeping>?> GetListAsync(
        Expression<Func<Timekeeping, bool>>? predicate = null,
        Func<IQueryable<Timekeeping>, IOrderedQueryable<Timekeeping>>? orderBy = null,
        Func<IQueryable<Timekeeping>, IIncludableQueryable<Timekeeping, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Timekeeping> AddAsync(Timekeeping timekeeping);
    Task<Timekeeping> UpdateAsync(Timekeeping timekeeping);
    Task<Timekeeping> DeleteAsync(Timekeeping timekeeping, bool permanent = false);
}
