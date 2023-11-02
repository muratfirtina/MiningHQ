using Application.Features.Maintenances.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Queries.GetList;

public class GetListMaintenanceQuery : IRequest<GetListResponse<GetListMaintenanceListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMaintenances({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[] {"GetMaintenances"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMaintenanceQueryHandler : IRequestHandler<GetListMaintenanceQuery, GetListResponse<GetListMaintenanceListItemDto>>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;

        public GetListMaintenanceQueryHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper)
        {
            _maintenanceRepository = maintenanceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMaintenanceListItemDto>> Handle(GetListMaintenanceQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Maintenance> maintenances = await _maintenanceRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMaintenanceListItemDto> response = _mapper.Map<GetListResponse<GetListMaintenanceListItemDto>>(maintenances);
            return response;
        }
    }
}