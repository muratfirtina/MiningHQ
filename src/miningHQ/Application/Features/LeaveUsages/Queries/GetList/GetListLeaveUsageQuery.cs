using Application.Features.LeaveUsages.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LeaveUsages.Constants.LeaveUsagesOperationClaims;

namespace Application.Features.LeaveUsages.Queries.GetList;

public class GetListLeaveUsageQuery : IRequest<GetListResponse<GetListLeaveUsageListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLeaveUsages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLeaveUsages";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLeaveUsageQueryHandler : IRequestHandler<GetListLeaveUsageQuery, GetListResponse<GetListLeaveUsageListItemDto>>
    {
        private readonly ILeaveUsageRepository _leaveUsageRepository;
        private readonly IMapper _mapper;

        public GetListLeaveUsageQueryHandler(ILeaveUsageRepository leaveUsageRepository, IMapper mapper)
        {
            _leaveUsageRepository = leaveUsageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLeaveUsageListItemDto>> Handle(GetListLeaveUsageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LeaveUsage> leaveUsages = await _leaveUsageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLeaveUsageListItemDto> response = _mapper.Map<GetListResponse<GetListLeaveUsageListItemDto>>(leaveUsages);
            return response;
        }
    }
}