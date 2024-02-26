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
using Microsoft.EntityFrameworkCore;
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
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                var entitledLeavesList = await _entitledLeaveRepository.GetAllAsync(
                    include: el => el.Include(e => e.Employee).Include(lt => lt.LeaveType),
                    cancellationToken: cancellationToken
                );

                var employeeEntitledLeaves = _mapper.Map<List<GetListEntitledLeaveListItemDto>>(entitledLeavesList);
                return new GetListResponse<GetListEntitledLeaveListItemDto>
                {
                    Items = employeeEntitledLeaves,
                    Index = -1,
                    Size = -1,
                    Count = entitledLeavesList.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
                
            }
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