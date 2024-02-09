using Application.Features.EntitledLeaves.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EntitledLeaves;

public class EntitledLeavesManager : IEntitledLeavesService
{
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;
    private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

    public EntitledLeavesManager(IEntitledLeaveRepository entitledLeaveRepository, EntitledLeaveBusinessRules entitledLeaveBusinessRules)
    {
        _entitledLeaveRepository = entitledLeaveRepository;
        _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
    }

    public async Task<EntitledLeave?> GetAsync(
        Expression<Func<EntitledLeave, bool>> predicate,
        Func<IQueryable<EntitledLeave>, IIncludableQueryable<EntitledLeave, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        EntitledLeave? entitledLeave = await _entitledLeaveRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return entitledLeave;
    }

    public async Task<IPaginate<EntitledLeave>?> GetListAsync(
        Expression<Func<EntitledLeave, bool>>? predicate = null,
        Func<IQueryable<EntitledLeave>, IOrderedQueryable<EntitledLeave>>? orderBy = null,
        Func<IQueryable<EntitledLeave>, IIncludableQueryable<EntitledLeave, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<EntitledLeave> entitledLeaveList = await _entitledLeaveRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return entitledLeaveList;
    }

    public async Task<EntitledLeave> AddAsync(EntitledLeave entitledLeave)
    {
        EntitledLeave addedEntitledLeave = await _entitledLeaveRepository.AddAsync(entitledLeave);

        return addedEntitledLeave;
    }

    public async Task<EntitledLeave> UpdateAsync(EntitledLeave entitledLeave)
    {
        EntitledLeave updatedEntitledLeave = await _entitledLeaveRepository.UpdateAsync(entitledLeave);

        return updatedEntitledLeave;
    }

    public async Task<EntitledLeave> DeleteAsync(EntitledLeave entitledLeave, bool permanent = false)
    {
        EntitledLeave deletedEntitledLeave = await _entitledLeaveRepository.DeleteAsync(entitledLeave);

        return deletedEntitledLeave;
    }

    public async Task<int?> GetRemainingEntitledLeavesAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _entitledLeaveBusinessRules.CalculateRemainingAnnualLeaves(employeeId, cancellationToken);
    }
}
