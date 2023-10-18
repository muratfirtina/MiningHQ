using Application.Features.Jobs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Jobs;

public class JobsManager : IJobsService
{
    private readonly IJobRepository _jobRepository;
    private readonly JobBusinessRules _jobBusinessRules;

    public JobsManager(IJobRepository jobRepository, JobBusinessRules jobBusinessRules)
    {
        _jobRepository = jobRepository;
        _jobBusinessRules = jobBusinessRules;
    }

    public async Task<Job?> GetAsync(
        Expression<Func<Job, bool>> predicate,
        Func<IQueryable<Job>, IIncludableQueryable<Job, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Job? job = await _jobRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return job;
    }

    public async Task<IPaginate<Job>?> GetListAsync(
        Expression<Func<Job, bool>>? predicate = null,
        Func<IQueryable<Job>, IOrderedQueryable<Job>>? orderBy = null,
        Func<IQueryable<Job>, IIncludableQueryable<Job, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Job> jobList = await _jobRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return jobList;
    }

    public async Task<Job> AddAsync(Job job)
    {
        Job addedJob = await _jobRepository.AddAsync(job);

        return addedJob;
    }

    public async Task<Job> UpdateAsync(Job job)
    {
        Job updatedJob = await _jobRepository.UpdateAsync(job);

        return updatedJob;
    }

    public async Task<Job> DeleteAsync(Job job, bool permanent = false)
    {
        Job deletedJob = await _jobRepository.DeleteAsync(job);

        return deletedJob;
    }
}
