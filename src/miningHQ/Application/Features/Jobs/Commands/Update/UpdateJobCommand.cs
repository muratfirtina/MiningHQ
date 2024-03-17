using Application.Features.Jobs.Constants;
using Application.Features.Jobs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Jobs.Constants.JobsOperationClaims;

namespace Application.Features.Jobs.Commands.Update;

public class UpdateJobCommand : IRequest<UpdatedJobResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }
    public string[] Roles => new[] { Admin, Write, JobsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] {"GetJobs"};

    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, UpdatedJobResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly JobBusinessRules _jobBusinessRules;

        public UpdateJobCommandHandler(IMapper mapper, IJobRepository jobRepository,
                                         JobBusinessRules jobBusinessRules)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobBusinessRules = jobBusinessRules;
        }

        public async Task<UpdatedJobResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            Job? job = await _jobRepository.GetAsync(predicate: j => j.Id == request.Id,
                include: j => j.Include(j => j.Department), cancellationToken: cancellationToken);
            await _jobBusinessRules.JobShouldExistWhenSelected(job);
            job = _mapper.Map(request, job);

            await _jobRepository.UpdateAsync(job!);

            UpdatedJobResponse response = _mapper.Map<UpdatedJobResponse>(job);
            return response;
        }
    }
}