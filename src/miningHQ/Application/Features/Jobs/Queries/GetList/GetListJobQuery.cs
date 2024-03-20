using Application.Features.Jobs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Jobs.Constants.JobsOperationClaims;

namespace Application.Features.Jobs.Queries.GetList;

public class GetListJobQuery : IRequest<GetListResponse<GetListJobListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListJobs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[]{"GetJobs"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListJobQueryHandler : IRequestHandler<GetListJobQuery, GetListResponse<GetListJobListItemDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetListJobQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListJobListItemDto>> Handle(GetListJobQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                // Sayfalama yapmadan tüm listeyi çek
                var allJobs = await _jobRepository.GetAllAsync(
                    include:e => e.Include(e => e.Employees)
                        .Include(e => e.Department),
                    cancellationToken: cancellationToken
                    );
                var jobDtos = _mapper.Map<List<GetListJobListItemDto>>(allJobs);
                
                return new GetListResponse<GetListJobListItemDto>
                {
                    Items = jobDtos,
                    Index = -1,
                    Size = -1,
                    Count = allJobs.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
            }
            else
            {
                // Sayfalama işlemi
                IPaginate<Job> jobs = await _jobRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    orderBy: p => p.OrderBy(p => p.Department.Name),
                    include:e => e.Include(e => e.Employees).Include(e => e.Department),
                    
                    cancellationToken: cancellationToken
                );
                
                GetListResponse<GetListJobListItemDto> response = _mapper.Map<GetListResponse<GetListJobListItemDto>>(jobs);
                return response;
            }
            
            
        }
    }
}