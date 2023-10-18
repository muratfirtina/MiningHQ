using Application.Features.DailyWorkDatas.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DailyWorkDatas;

public class DailyWorkDatasManager : IDailyWorkDatasService
{
    private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
    private readonly DailyWorkDataBusinessRules _dailyWorkDataBusinessRules;

    public DailyWorkDatasManager(IDailyWorkDataRepository dailyWorkDataRepository, DailyWorkDataBusinessRules dailyWorkDataBusinessRules)
    {
        _dailyWorkDataRepository = dailyWorkDataRepository;
        _dailyWorkDataBusinessRules = dailyWorkDataBusinessRules;
    }

    public async Task<DailyWorkData?> GetAsync(
        Expression<Func<DailyWorkData, bool>> predicate,
        Func<IQueryable<DailyWorkData>, IIncludableQueryable<DailyWorkData, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DailyWorkData? dailyWorkData = await _dailyWorkDataRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return dailyWorkData;
    }

    public async Task<IPaginate<DailyWorkData>?> GetListAsync(
        Expression<Func<DailyWorkData, bool>>? predicate = null,
        Func<IQueryable<DailyWorkData>, IOrderedQueryable<DailyWorkData>>? orderBy = null,
        Func<IQueryable<DailyWorkData>, IIncludableQueryable<DailyWorkData, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DailyWorkData> dailyWorkDataList = await _dailyWorkDataRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dailyWorkDataList;
    }

    public async Task<DailyWorkData> AddAsync(DailyWorkData dailyWorkData)
    {
        DailyWorkData addedDailyWorkData = await _dailyWorkDataRepository.AddAsync(dailyWorkData);

        return addedDailyWorkData;
    }

    public async Task<DailyWorkData> UpdateAsync(DailyWorkData dailyWorkData)
    {
        DailyWorkData updatedDailyWorkData = await _dailyWorkDataRepository.UpdateAsync(dailyWorkData);

        return updatedDailyWorkData;
    }

    public async Task<DailyWorkData> DeleteAsync(DailyWorkData dailyWorkData, bool permanent = false)
    {
        DailyWorkData deletedDailyWorkData = await _dailyWorkDataRepository.DeleteAsync(dailyWorkData);

        return deletedDailyWorkData;
    }
}
