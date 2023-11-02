using Application.Features.Maintenances.Constants;
using Application.Features.Maintenances.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Commands.Create;

public class CreateMaintenanceCommand : IRequest<CreatedMaintenanceResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
    public Guid MaintenanceTypeId { get; set; }
    public MaintenanceType MaintenanceType { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int MachineWorkingTimeOrKilometer { get; set; }
    public string MaintenanceFirm { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenancesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetMaintenances"};

    public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, CreatedMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly MaintenanceBusinessRules _maintenanceBusinessRules;

        public CreateMaintenanceCommandHandler(IMapper mapper, IMaintenanceRepository maintenanceRepository,
                                         MaintenanceBusinessRules maintenanceBusinessRules)
        {
            _mapper = mapper;
            _maintenanceRepository = maintenanceRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
        }

        public async Task<CreatedMaintenanceResponse> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            Maintenance maintenance = _mapper.Map<Maintenance>(request);

            await _maintenanceRepository.AddAsync(maintenance);

            CreatedMaintenanceResponse response = _mapper.Map<CreatedMaintenanceResponse>(maintenance);
            return response;
        }
    }
}