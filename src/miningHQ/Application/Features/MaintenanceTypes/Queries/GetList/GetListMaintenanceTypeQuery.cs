using Application.Features.MaintenanceTypes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.MaintenanceTypes.Constants.MaintenanceTypesOperationClaims;

namespace Application.Features.MaintenanceTypes.Queries.GetList;

public class GetListMaintenanceTypeQuery : IRequest<GetListResponse<GetListMaintenanceTypeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMaintenanceTypes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMaintenanceTypes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMaintenanceTypeQueryHandler : IRequestHandler<GetListMaintenanceTypeQuery, GetListResponse<GetListMaintenanceTypeListItemDto>>
    {
        private readonly IMaintenanceTypeRepository _maintenanceTypeRepository;
        private readonly IMapper _mapper;

        public GetListMaintenanceTypeQueryHandler(IMaintenanceTypeRepository maintenanceTypeRepository, IMapper mapper)
        {
            _maintenanceTypeRepository = maintenanceTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMaintenanceTypeListItemDto>> Handle(GetListMaintenanceTypeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<MaintenanceType> maintenanceTypes = await _maintenanceTypeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMaintenanceTypeListItemDto> response = _mapper.Map<GetListResponse<GetListMaintenanceTypeListItemDto>>(maintenanceTypes);
            return response;
        }
    }
}