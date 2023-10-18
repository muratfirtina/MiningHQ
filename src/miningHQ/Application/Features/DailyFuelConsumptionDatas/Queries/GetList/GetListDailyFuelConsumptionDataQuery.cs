using Application.Features.DailyFuelConsumptionDatas.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.DailyFuelConsumptionDatas.Constants.DailyFuelConsumptionDatasOperationClaims;

namespace Application.Features.DailyFuelConsumptionDatas.Queries.GetList;

public class GetListDailyFuelConsumptionDataQuery : IRequest<GetListResponse<GetListDailyFuelConsumptionDataListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListDailyFuelConsumptionDatas({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetDailyFuelConsumptionDatas";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDailyFuelConsumptionDataQueryHandler : IRequestHandler<GetListDailyFuelConsumptionDataQuery, GetListResponse<GetListDailyFuelConsumptionDataListItemDto>>
    {
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly IMapper _mapper;

        public GetListDailyFuelConsumptionDataQueryHandler(IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository, IMapper mapper)
        {
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDailyFuelConsumptionDataListItemDto>> Handle(GetListDailyFuelConsumptionDataQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DailyFuelConsumptionData> dailyFuelConsumptionDatas = await _dailyFuelConsumptionDataRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDailyFuelConsumptionDataListItemDto> response = _mapper.Map<GetListResponse<GetListDailyFuelConsumptionDataListItemDto>>(dailyFuelConsumptionDatas);
            return response;
        }
    }
}