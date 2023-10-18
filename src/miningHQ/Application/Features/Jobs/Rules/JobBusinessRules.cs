using Application.Features.Jobs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Jobs.Rules;

public class JobBusinessRules : BaseBusinessRules
{
    private readonly IJobRepository _jobRepository;

    public JobBusinessRules(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public Task JobShouldExistWhenSelected(Job? job)
    {
        if (job == null)
            throw new BusinessException(JobsBusinessMessages.JobNotExists);
        return Task.CompletedTask;
    }

    public async Task JobIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Job? job = await _jobRepository.GetAsync(
            predicate: j => j.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await JobShouldExistWhenSelected(job);
    }
}