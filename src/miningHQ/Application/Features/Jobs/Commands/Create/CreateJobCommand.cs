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
using static Application.Features.Jobs.Constants.JobsOperationClaims;

namespace Application.Features.Jobs.Commands.Create;

public class CreateJobCommand : IRequest<CreatedJobResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string? DepartmentId { get; set; }

    public string[] Roles => new[] { Admin, Write, JobsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetJobs"};

    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CreatedJobResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly JobBusinessRules _jobBusinessRules;

        public CreateJobCommandHandler(IMapper mapper, IJobRepository jobRepository,
                                         JobBusinessRules jobBusinessRules)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobBusinessRules = jobBusinessRules;
        }

        public async Task<CreatedJobResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            Job job = _mapper.Map<Job>(request);

            await _jobRepository.AddAsync(job);

            CreatedJobResponse response = _mapper.Map<CreatedJobResponse>(job);
            return response;
        }
    }
}