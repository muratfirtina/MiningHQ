using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Maintenances.Queries.GetListByMachineId;

public class GetListMaintenanceByMachineIdQuery : IRequest<GetListResponse<GetListMaintenanceByMachineIdListItemDto>>
{
    public Guid MachineId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListMaintenanceByMachineIdQueryHandler : IRequestHandler<GetListMaintenanceByMachineIdQuery, GetListResponse<GetListMaintenanceByMachineIdListItemDto>>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;

        public GetListMaintenanceByMachineIdQueryHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper)
        {
            _maintenanceRepository = maintenanceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMaintenanceByMachineIdListItemDto>> Handle(GetListMaintenanceByMachineIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Maintenance> maintenances = await _maintenanceRepository.GetListAsync(
                predicate: m => m.MachineId == request.MachineId,
                include: m => m
                    .Include(m => m.Machine)
                    .Include(m => m.MaintenanceType)
                    .Include(m => m.MaintenanceFiles),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: query => query.OrderByDescending(m => m.MaintenanceDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMaintenanceByMachineIdListItemDto> response = _mapper.Map<GetListResponse<GetListMaintenanceByMachineIdListItemDto>>(maintenances);
            return response;
        }
    }
}
