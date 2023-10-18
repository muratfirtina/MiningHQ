using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.MachineTypes;

public interface IMachineTypesService
{
    Task<MachineType?> GetAsync(
        Expression<Func<MachineType, bool>> predicate,
        Func<IQueryable<MachineType>, IIncludableQueryable<MachineType, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<MachineType>?> GetListAsync(
        Expression<Func<MachineType, bool>>? predicate = null,
        Func<IQueryable<MachineType>, IOrderedQueryable<MachineType>>? orderBy = null,
        Func<IQueryable<MachineType>, IIncludableQueryable<MachineType, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<MachineType> AddAsync(MachineType machineType);
    Task<MachineType> UpdateAsync(MachineType machineType);
    Task<MachineType> DeleteAsync(MachineType machineType, bool permanent = false);
}
