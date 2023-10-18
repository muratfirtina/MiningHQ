using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Jobs;

public interface IJobsService
{
    Task<Job?> GetAsync(
        Expression<Func<Job, bool>> predicate,
        Func<IQueryable<Job>, IIncludableQueryable<Job, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Job>?> GetListAsync(
        Expression<Func<Job, bool>>? predicate = null,
        Func<IQueryable<Job>, IOrderedQueryable<Job>>? orderBy = null,
        Func<IQueryable<Job>, IIncludableQueryable<Job, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Job> AddAsync(Job job);
    Task<Job> UpdateAsync(Job job);
    Task<Job> DeleteAsync(Job job, bool permanent = false);
}
