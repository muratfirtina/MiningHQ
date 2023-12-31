using Application.Features.MachineTypes.Constants;
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
using static Application.Features.MachineTypes.Constants.MachineTypesOperationClaims;

namespace Application.Features.MachineTypes.Queries.GetList;

public class GetListMachineTypeQuery : IRequest<GetListResponse<GetListMachineTypeListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMachineTypes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[] {"GetMachineTypes"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMachineTypeQueryHandler : IRequestHandler<GetListMachineTypeQuery, GetListResponse<GetListMachineTypeListItemDto>>
    {
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly IMapper _mapper;

        public GetListMachineTypeQueryHandler(IMachineTypeRepository machineTypeRepository, IMapper mapper)
        {
            _machineTypeRepository = machineTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMachineTypeListItemDto>> Handle(GetListMachineTypeQuery request, CancellationToken cancellationToken)
        {
            if(request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                var allMachineTypes = await _machineTypeRepository.GetAllAsync();
                var machineTypeDto = _mapper.Map<List<GetListMachineTypeListItemDto>>(allMachineTypes);

                return new GetListResponse<GetListMachineTypeListItemDto>
                {
                    Items = machineTypeDto,
                    Index = -1,
                    Size = -1,
                    Count = allMachineTypes.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
            }
            else
            {
                IPaginate<MachineType> machineTypes = await _machineTypeRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListMachineTypeListItemDto> response = _mapper.Map<GetListResponse<GetListMachineTypeListItemDto>>(machineTypes);
                return response;
            }
        }
    }
}