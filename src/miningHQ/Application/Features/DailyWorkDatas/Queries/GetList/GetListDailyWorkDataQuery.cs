using Application.Features.DailyWorkDatas.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.DailyWorkDatas.Constants.DailyWorkDatasOperationClaims;

namespace Application.Features.DailyWorkDatas.Queries.GetList;

public class GetListDailyWorkDataQuery : IRequest<GetListResponse<GetListDailyWorkDataListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListDailyWorkDatas({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetDailyWorkDatas";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDailyWorkDataQueryHandler : IRequestHandler<GetListDailyWorkDataQuery, GetListResponse<GetListDailyWorkDataListItemDto>>
    {
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly IMapper _mapper;

        public GetListDailyWorkDataQueryHandler(IDailyWorkDataRepository dailyWorkDataRepository, IMapper mapper)
        {
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDailyWorkDataListItemDto>> Handle(GetListDailyWorkDataQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DailyWorkData> dailyWorkDatas = await _dailyWorkDataRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDailyWorkDataListItemDto> response = _mapper.Map<GetListResponse<GetListDailyWorkDataListItemDto>>(dailyWorkDatas);
            return response;
        }
    }
}