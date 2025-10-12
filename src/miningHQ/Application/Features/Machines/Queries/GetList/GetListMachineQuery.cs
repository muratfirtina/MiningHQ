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
using Microsoft.EntityFrameworkCore;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Queries.GetList;

public class GetListMachineQuery : IRequest<GetListResponse<GetListMachineListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMachines({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[] {"GetMachines"};
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
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                
                var allMachines = await _machineRepository.GetAllAsync(
                    include:m => m.Include(m => m.Model)
                                  .Include(m => m.Quarry)
                                  .Include(m => m.MachineType)
                                  .Include(m => m.Model.Brand)
                                  .Include(m => m.CurrentOperator)
                                  .Include(m => m.DailyWorkDatas)
                                  .Include(m => m.Maintenances)
                    );
                    
                var machineDto = _mapper.Map<List<GetListMachineListItemDto>>(allMachines);

                return new GetListResponse<GetListMachineListItemDto>
                {
                    Items = machineDto,
                    Index = -1,
                    Size = -1,
                    Count = allMachines.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
            }
            else
            {
                
                IPaginate<Machine> machines = await _machineRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include:m => m.Include(m => m.Model)
                                  .Include(m => m.Quarry)
                                  .Include(m => m.MachineType)
                                  .Include(m => m.Model.Brand)
                                  .Include(m => m.CurrentOperator)
                                  .Include(m => m.DailyWorkDatas)
                                  .Include(m => m.Maintenances),
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListMachineListItemDto> response = _mapper.Map<GetListResponse<GetListMachineListItemDto>>(machines);
                return response;
            }
        }
    }
}