using Application.Features.LeaveTypes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LeaveUsages;

public class LeaveUsagesManager : ILeaveUsagesService
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly LeaveUsageTypeBusinessRules _leaveUsageTypeBusinessRules;

    public LeaveUsagesManager(ILeaveTypeRepository leaveTypeRepository, LeaveUsageTypeBusinessRules leaveUsageTypeBusinessRules)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveUsageTypeBusinessRules = leaveUsageTypeBusinessRules;
    }

    public async Task<LeaveType?> GetAsync(
        Expression<Func<LeaveType, bool>> predicate,
        Func<IQueryable<LeaveType>, IIncludableQueryable<LeaveType, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LeaveType? leaveUsage = await _leaveTypeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return leaveUsage;
    }

    public async Task<IPaginate<LeaveType>?> GetListAsync(
        Expression<Func<LeaveType, bool>>? predicate = null,
        Func<IQueryable<LeaveType>, IOrderedQueryable<LeaveType>>? orderBy = null,
        Func<IQueryable<LeaveType>, IIncludableQueryable<LeaveType, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LeaveType> leaveUsageList = await _leaveTypeRepository.GetListAsync(
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

    public async Task<LeaveType> AddAsync(LeaveType leaveType)
    {
        LeaveType addedLeaveType = await _leaveTypeRepository.AddAsync(leaveType);

        return addedLeaveType;
    }

    public async Task<LeaveType> UpdateAsync(LeaveType leaveType)
    {
        LeaveType updatedLeaveType = await _leaveTypeRepository.UpdateAsync(leaveType);

        return updatedLeaveType;
    }

    public async Task<LeaveType> DeleteAsync(LeaveType leaveType, bool permanent = false)
    {
        LeaveType deletedLeaveType = await _leaveTypeRepository.DeleteAsync(leaveType);

        return deletedLeaveType;
    }
}
