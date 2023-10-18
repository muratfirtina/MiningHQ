using Application.Features.Quarries.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Quarries;

public class QuarriesManager : IQuarriesService
{
    private readonly IQuarryRepository _quarryRepository;
    private readonly QuarryBusinessRules _quarryBusinessRules;

    public QuarriesManager(IQuarryRepository quarryRepository, QuarryBusinessRules quarryBusinessRules)
    {
        _quarryRepository = quarryRepository;
        _quarryBusinessRules = quarryBusinessRules;
    }

    public async Task<Quarry?> GetAsync(
        Expression<Func<Quarry, bool>> predicate,
        Func<IQueryable<Quarry>, IIncludableQueryable<Quarry, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Quarry? quarry = await _quarryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return quarry;
    }

    public async Task<IPaginate<Quarry>?> GetListAsync(
        Expression<Func<Quarry, bool>>? predicate = null,
        Func<IQueryable<Quarry>, IOrderedQueryable<Quarry>>? orderBy = null,
        Func<IQueryable<Quarry>, IIncludableQueryable<Quarry, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Quarry> quarryList = await _quarryRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return quarryList;
    }

    public async Task<Quarry> AddAsync(Quarry quarry)
    {
        Quarry addedQuarry = await _quarryRepository.AddAsync(quarry);

        return addedQuarry;
    }

    public async Task<Quarry> UpdateAsync(Quarry quarry)
    {
        Quarry updatedQuarry = await _quarryRepository.UpdateAsync(quarry);

        return updatedQuarry;
    }

    public async Task<Quarry> DeleteAsync(Quarry quarry, bool permanent = false)
    {
        Quarry deletedQuarry = await _quarryRepository.DeleteAsync(quarry);

        return deletedQuarry;
    }
}
