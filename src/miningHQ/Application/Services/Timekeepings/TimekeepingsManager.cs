using Application.Features.Timekeepings.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Timekeepings;

public class TimekeepingsManager : ITimekeepingsService
{
    private readonly ITimekeepingRepository _timekeepingRepository;
    private readonly TimekeepingBusinessRules _timekeepingBusinessRules;

    public TimekeepingsManager(ITimekeepingRepository timekeepingRepository, TimekeepingBusinessRules timekeepingBusinessRules)
    {
        _timekeepingRepository = timekeepingRepository;
        _timekeepingBusinessRules = timekeepingBusinessRules;
    }

    public async Task<Timekeeping?> GetAsync(
        Expression<Func<Timekeeping, bool>> predicate,
        Func<IQueryable<Timekeeping>, IIncludableQueryable<Timekeeping, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Timekeeping? timekeeping = await _timekeepingRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return timekeeping;
    }

    public async Task<IPaginate<Timekeeping>?> GetListAsync(
        Expression<Func<Timekeeping, bool>>? predicate = null,
        Func<IQueryable<Timekeeping>, IOrderedQueryable<Timekeeping>>? orderBy = null,
        Func<IQueryable<Timekeeping>, IIncludableQueryable<Timekeeping, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Timekeeping> timekeepingList = await _timekeepingRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return timekeepingList;
    }

    public async Task<Timekeeping> AddAsync(Timekeeping timekeeping)
    {
        Timekeeping addedTimekeeping = await _timekeepingRepository.AddAsync(timekeeping);

        return addedTimekeeping;
    }

    public async Task<Timekeeping> UpdateAsync(Timekeeping timekeeping)
    {
        Timekeeping updatedTimekeeping = await _timekeepingRepository.UpdateAsync(timekeeping);

        return updatedTimekeeping;
    }

    public async Task<Timekeeping> DeleteAsync(Timekeeping timekeeping, bool permanent = false)
    {
        Timekeeping deletedTimekeeping = await _timekeepingRepository.DeleteAsync(timekeeping);

        return deletedTimekeeping;
    }
}
