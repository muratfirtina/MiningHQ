using Application.Features.Overtimes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Overtimes.Constants.OvertimesOperationClaims;

namespace Application.Features.Overtimes.Queries.GetList;

public class GetListOvertimeQuery : IRequest<GetListResponse<GetListOvertimeListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListOvertimes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetOvertimes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListOvertimeQueryHandler : IRequestHandler<GetListOvertimeQuery, GetListResponse<GetListOvertimeListItemDto>>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetListOvertimeQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOvertimeListItemDto>> Handle(GetListOvertimeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Overtime> overtimes = await _overtimeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOvertimeListItemDto> response = _mapper.Map<GetListResponse<GetListOvertimeListItemDto>>(overtimes);
            return response;
        }
    }
}