using Application.Features.DailyFuelConsumptionDatas.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DailyFuelConsumptionDatas;

public class DailyFuelConsumptionDatasManager : IDailyFuelConsumptionDatasService
{
    private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
    private readonly DailyFuelConsumptionDataBusinessRules _dailyFuelConsumptionDataBusinessRules;

    public DailyFuelConsumptionDatasManager(IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository, DailyFuelConsumptionDataBusinessRules dailyFuelConsumptionDataBusinessRules)
    {
        _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
        _dailyFuelConsumptionDataBusinessRules = dailyFuelConsumptionDataBusinessRules;
    }

    public async Task<DailyFuelConsumptionData?> GetAsync(
        Expression<Func<DailyFuelConsumptionData, bool>> predicate,
        Func<IQueryable<DailyFuelConsumptionData>, IIncludableQueryable<DailyFuelConsumptionData, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DailyFuelConsumptionData? dailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return dailyFuelConsumptionData;
    }

    public async Task<IPaginate<DailyFuelConsumptionData>?> GetListAsync(
        Expression<Func<DailyFuelConsumptionData, bool>>? predicate = null,
        Func<IQueryable<DailyFuelConsumptionData>, IOrderedQueryable<DailyFuelConsumptionData>>? orderBy = null,
        Func<IQueryable<DailyFuelConsumptionData>, IIncludableQueryable<DailyFuelConsumptionData, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DailyFuelConsumptionData> dailyFuelConsumptionDataList = await _dailyFuelConsumptionDataRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dailyFuelConsumptionDataList;
    }

    public async Task<DailyFuelConsumptionData> AddAsync(DailyFuelConsumptionData dailyFuelConsumptionData)
    {
        DailyFuelConsumptionData addedDailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.AddAsync(dailyFuelConsumptionData);

        return addedDailyFuelConsumptionData;
    }

    public async Task<DailyFuelConsumptionData> UpdateAsync(DailyFuelConsumptionData dailyFuelConsumptionData)
    {
        DailyFuelConsumptionData updatedDailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.UpdateAsync(dailyFuelConsumptionData);

        return updatedDailyFuelConsumptionData;
    }

    public async Task<DailyFuelConsumptionData> DeleteAsync(DailyFuelConsumptionData dailyFuelConsumptionData, bool permanent = false)
    {
        DailyFuelConsumptionData deletedDailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.DeleteAsync(dailyFuelConsumptionData);

        return deletedDailyFuelConsumptionData;
    }
}
