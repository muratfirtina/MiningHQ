using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Machines;

public interface IMachinesService
{
    Task<Machine?> GetAsync(
        Expression<Func<Machine, bool>> predicate,
        Func<IQueryable<Machine>, IIncludableQueryable<Machine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Machine>?> GetListAsync(
        Expression<Func<Machine, bool>>? predicate = null,
        Func<IQueryable<Machine>, IOrderedQueryable<Machine>>? orderBy = null,
        Func<IQueryable<Machine>, IIncludableQueryable<Machine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Machine> AddAsync(Machine machine);
    Task<Machine> UpdateAsync(Machine machine);
    Task<Machine> DeleteAsync(Machine machine, bool permanent = false);
}
