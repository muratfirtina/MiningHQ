using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EntitledLeaves;

public interface IEntitledLeavesService
{
    Task<EntitledLeave?> GetAsync(
        Expression<Func<EntitledLeave, bool>> predicate,
        Func<IQueryable<EntitledLeave>, IIncludableQueryable<EntitledLeave, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<EntitledLeave>?> GetListAsync(
        Expression<Func<EntitledLeave, bool>>? predicate = null,
        Func<IQueryable<EntitledLeave>, IOrderedQueryable<EntitledLeave>>? orderBy = null,
        Func<IQueryable<EntitledLeave>, IIncludableQueryable<EntitledLeave, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<EntitledLeave> AddAsync(EntitledLeave entitledLeave);
    Task<EntitledLeave> UpdateAsync(EntitledLeave entitledLeave);
    Task<EntitledLeave> DeleteAsync(EntitledLeave entitledLeave, bool permanent = false);
}
