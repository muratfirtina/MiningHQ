using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DailyFuelConsumptionDatas;

public interface IDailyFuelConsumptionDatasService
{
    Task<DailyFuelConsumptionData?> GetAsync(
        Expression<Func<DailyFuelConsumptionData, bool>> predicate,
        Func<IQueryable<DailyFuelConsumptionData>, IIncludableQueryable<DailyFuelConsumptionData, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DailyFuelConsumptionData>?> GetListAsync(
        Expression<Func<DailyFuelConsumptionData, bool>>? predicate = null,
        Func<IQueryable<DailyFuelConsumptionData>, IOrderedQueryable<DailyFuelConsumptionData>>? orderBy = null,
        Func<IQueryable<DailyFuelConsumptionData>, IIncludableQueryable<DailyFuelConsumptionData, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DailyFuelConsumptionData> AddAsync(DailyFuelConsumptionData dailyFuelConsumptionData);
    Task<DailyFuelConsumptionData> UpdateAsync(DailyFuelConsumptionData dailyFuelConsumptionData);
    Task<DailyFuelConsumptionData> DeleteAsync(DailyFuelConsumptionData dailyFuelConsumptionData, bool permanent = false);
}
