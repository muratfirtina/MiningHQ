using Application.Features.Maintenances.Constants;
using Application.Features.Maintenances.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Queries.GetById;

public class GetByIdMaintenanceQuery : IRequest<GetByIdMaintenanceResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMaintenanceQueryHandler : IRequestHandler<GetByIdMaintenanceQuery, GetByIdMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly MaintenanceBusinessRules _maintenanceBusinessRules;

        public GetByIdMaintenanceQueryHandler(IMapper mapper, IMaintenanceRepository maintenanceRepository, MaintenanceBusinessRules maintenanceBusinessRules)
        {
            _mapper = mapper;
            _maintenanceRepository = maintenanceRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
        }

        public async Task<GetByIdMaintenanceResponse> Handle(GetByIdMaintenanceQuery request, CancellationToken cancellationToken)
        {
            Maintenance? maintenance = await _maintenanceRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _maintenanceBusinessRules.MaintenanceShouldExistWhenSelected(maintenance);

            GetByIdMaintenanceResponse response = _mapper.Map<GetByIdMaintenanceResponse>(maintenance);
            return response;
        }
    }
}