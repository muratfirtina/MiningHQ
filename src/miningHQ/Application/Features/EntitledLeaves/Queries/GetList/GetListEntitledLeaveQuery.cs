using Application.Features.EntitledLeaves.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.EntitledLeaves.Constants.EntitledLeavesOperationClaims;

namespace Application.Features.EntitledLeaves.Queries.GetList;

public class GetListEntitledLeaveQuery : IRequest<GetListResponse<GetListEntitledLeaveListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEntitledLeaves({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetEntitledLeaves";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListEntitledLeaveQueryHandler : IRequestHandler<GetListEntitledLeaveQuery, GetListResponse<GetListEntitledLeaveListItemDto>>
    {
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly IMapper _mapper;

        public GetListEntitledLeaveQueryHandler(IEntitledLeaveRepository entitledLeaveRepository, IMapper mapper)
        {
            _entitledLeaveRepository = entitledLeaveRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEntitledLeaveListItemDto>> Handle(GetListEntitledLeaveQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EntitledLeave> entitledLeaves = await _entitledLeaveRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEntitledLeaveListItemDto> response = _mapper.Map<GetListResponse<GetListEntitledLeaveListItemDto>>(entitledLeaves);
            return response;
        }
    }
}