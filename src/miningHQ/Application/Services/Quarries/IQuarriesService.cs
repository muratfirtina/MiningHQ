using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Quarries;

public interface IQuarriesService
{
    Task<Quarry?> GetAsync(
        Expression<Func<Quarry, bool>> predicate,
        Func<IQueryable<Quarry>, IIncludableQueryable<Quarry, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Quarry>?> GetListAsync(
        Expression<Func<Quarry, bool>>? predicate = null,
        Func<IQueryable<Quarry>, IOrderedQueryable<Quarry>>? orderBy = null,
        Func<IQueryable<Quarry>, IIncludableQueryable<Quarry, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Quarry> AddAsync(Quarry quarry);
    Task<Quarry> UpdateAsync(Quarry quarry);
    Task<Quarry> DeleteAsync(Quarry quarry, bool permanent = false);
}
