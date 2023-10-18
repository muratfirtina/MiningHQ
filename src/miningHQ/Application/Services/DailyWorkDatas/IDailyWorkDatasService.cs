using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DailyWorkDatas;

public interface IDailyWorkDatasService
{
    Task<DailyWorkData?> GetAsync(
        Expression<Func<DailyWorkData, bool>> predicate,
        Func<IQueryable<DailyWorkData>, IIncludableQueryable<DailyWorkData, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DailyWorkData>?> GetListAsync(
        Expression<Func<DailyWorkData, bool>>? predicate = null,
        Func<IQueryable<DailyWorkData>, IOrderedQueryable<DailyWorkData>>? orderBy = null,
        Func<IQueryable<DailyWorkData>, IIncludableQueryable<DailyWorkData, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DailyWorkData> AddAsync(DailyWorkData dailyWorkData);
    Task<DailyWorkData> UpdateAsync(DailyWorkData dailyWorkData);
    Task<DailyWorkData> DeleteAsync(DailyWorkData dailyWorkData, bool permanent = false);
}
