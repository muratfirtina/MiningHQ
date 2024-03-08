using Application.Features.Overtimes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Overtimes;

public class OvertimesManager : IOvertimesService
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly OvertimeBusinessRules _overtimeBusinessRules;

    public OvertimesManager(IOvertimeRepository overtimeRepository, OvertimeBusinessRules overtimeBusinessRules)
    {
        _overtimeRepository = overtimeRepository;
        _overtimeBusinessRules = overtimeBusinessRules;
    }

    public async Task<Overtime?> GetAsync(
        Expression<Func<Overtime, bool>> predicate,
        Func<IQueryable<Overtime>, IIncludableQueryable<Overtime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Overtime? overtime = await _overtimeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return overtime;
    }

    public async Task<IPaginate<Overtime>?> GetListAsync(
        Expression<Func<Overtime, bool>>? predicate = null,
        Func<IQueryable<Overtime>, IOrderedQueryable<Overtime>>? orderBy = null,
        Func<IQueryable<Overtime>, IIncludableQueryable<Overtime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Overtime> overtimeList = await _overtimeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return overtimeList;
    }

    public async Task<Overtime> AddAsync(Overtime overtime)
    {
        Overtime addedOvertime = await _overtimeRepository.AddAsync(overtime);

        return addedOvertime;
    }

    public async Task<Overtime> UpdateAsync(Overtime overtime)
    {
        Overtime updatedOvertime = await _overtimeRepository.UpdateAsync(overtime);

        return updatedOvertime;
    }

    public async Task<Overtime> DeleteAsync(Overtime overtime, bool permanent = false)
    {
        Overtime deletedOvertime = await _overtimeRepository.DeleteAsync(overtime);

        return deletedOvertime;
    }
}
