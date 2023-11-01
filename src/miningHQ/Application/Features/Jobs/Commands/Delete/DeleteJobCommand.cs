using Application.Features.Jobs.Constants;
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

namespace Application.Features.Jobs.Commands.Delete;

public class DeleteJobCommand : IRequest<DeletedJobResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, JobsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetJobs";

    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, DeletedJobResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly JobBusinessRules _jobBusinessRules;

        public DeleteJobCommandHandler(IMapper mapper, IJobRepository jobRepository,
                                         JobBusinessRules jobBusinessRules)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobBusinessRules = jobBusinessRules;
        }

        public async Task<DeletedJobResponse> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            Job? job = await _jobRepository.GetAsync(predicate: j => j.Id == request.Id, cancellationToken: cancellationToken);
            await _jobBusinessRules.JobShouldExistWhenSelected(job);

            await _jobRepository.DeleteAsync(job!);

            DeletedJobResponse response = _mapper.Map<DeletedJobResponse>(job);
            return response;
        }
    }
}