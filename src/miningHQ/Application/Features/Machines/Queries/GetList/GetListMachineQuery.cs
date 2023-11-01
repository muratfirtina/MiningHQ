using Application.Features.Machines.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Queries.GetList;

public class GetListMachineQuery : IRequest<GetListResponse<GetListMachineListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMachines({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMachines";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMachineQueryHandler : IRequestHandler<GetListMachineQuery, GetListResponse<GetListMachineListItemDto>>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public GetListMachineQueryHandler(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMachineListItemDto>> Handle(GetListMachineQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Machine> machines = await _machineRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMachineListItemDto> response = _mapper.Map<GetListResponse<GetListMachineListItemDto>>(machines);
            return response;
        }
    }
}