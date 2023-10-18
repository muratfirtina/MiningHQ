using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Maintenances;

public interface IMaintenancesService
{
    Task<Maintenance?> GetAsync(
        Expression<Func<Maintenance, bool>> predicate,
        Func<IQueryable<Maintenance>, IIncludableQueryable<Maintenance, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Maintenance>?> GetListAsync(
        Expression<Func<Maintenance, bool>>? predicate = null,
        Func<IQueryable<Maintenance>, IOrderedQueryable<Maintenance>>? orderBy = null,
        Func<IQueryable<Maintenance>, IIncludableQueryable<Maintenance, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Maintenance> AddAsync(Maintenance maintenance);
    Task<Maintenance> UpdateAsync(Maintenance maintenance);
    Task<Maintenance> DeleteAsync(Maintenance maintenance, bool permanent = false);
}
