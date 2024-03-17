using Application.Features.Jobs.Constants;
using Application.Features.Jobs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Jobs.Constants.JobsOperationClaims;

namespace Application.Features.Jobs.Queries.GetById;

public class GetByIdJobQuery : IRequest<GetByIdJobResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdJobQueryHandler : IRequestHandler<GetByIdJobQuery, GetByIdJobResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly JobBusinessRules _jobBusinessRules;

        public GetByIdJobQueryHandler(IMapper mapper, IJobRepository jobRepository, JobBusinessRules jobBusinessRules)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobBusinessRules = jobBusinessRules;
        }

        public async Task<GetByIdJobResponse> Handle(GetByIdJobQuery request, CancellationToken cancellationToken)
        {
            Job? job = await _jobRepository.GetAsync(predicate: j => j.Id == request.Id,
                include: j => j.Include(j => j.Department), 
                cancellationToken: cancellationToken);
            await _jobBusinessRules.JobShouldExistWhenSelected(job);

            GetByIdJobResponse response = _mapper.Map<GetByIdJobResponse>(job);
            return response;
        }
    }
}