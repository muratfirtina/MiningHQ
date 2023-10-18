using Application.Features.LeaveUsages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LeaveUsages;

public class LeaveUsagesManager : ILeaveUsagesService
{
    private readonly ILeaveUsageRepository _leaveUsageRepository;
    private readonly LeaveUsageBusinessRules _leaveUsageBusinessRules;

    public LeaveUsagesManager(ILeaveUsageRepository leaveUsageRepository, LeaveUsageBusinessRules leaveUsageBusinessRules)
    {
        _leaveUsageRepository = leaveUsageRepository;
        _leaveUsageBusinessRules = leaveUsageBusinessRules;
    }

    public async Task<LeaveUsage?> GetAsync(
        Expression<Func<LeaveUsage, bool>> predicate,
        Func<IQueryable<LeaveUsage>, IIncludableQueryable<LeaveUsage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LeaveUsage? leaveUsage = await _leaveUsageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return leaveUsage;
    }

    public async Task<IPaginate<LeaveUsage>?> GetListAsync(
        Expression<Func<LeaveUsage, bool>>? predicate = null,
        Func<IQueryable<LeaveUsage>, IOrderedQueryable<LeaveUsage>>? orderBy = null,
        Func<IQueryable<LeaveUsage>, IIncludableQueryable<LeaveUsage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LeaveUsage> leaveUsageList = await _leaveUsageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return leaveUsageList;
    }

    public async Task<LeaveUsage> AddAsync(LeaveUsage leaveUsage)
    {
        LeaveUsage addedLeaveUsage = await _leaveUsageRepository.AddAsync(leaveUsage);

        return addedLeaveUsage;
    }

    public async Task<LeaveUsage> UpdateAsync(LeaveUsage leaveUsage)
    {
        LeaveUsage updatedLeaveUsage = await _leaveUsageRepository.UpdateAsync(leaveUsage);

        return updatedLeaveUsage;
    }

    public async Task<LeaveUsage> DeleteAsync(LeaveUsage leaveUsage, bool permanent = false)
    {
        LeaveUsage deletedLeaveUsage = await _leaveUsageRepository.DeleteAsync(leaveUsage);

        return deletedLeaveUsage;
    }
}
