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

namespace Application.Features.Maintenances.Commands.Update;

public class UpdateMaintenanceCommand : IRequest<UpdatedMaintenanceResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
    public Guid MaintenanceTypeId { get; set; }
    public MaintenanceType MaintenanceType { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int MachineWorkingTimeOrKilometer { get; set; }
    public string MaintenanceFirm { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenancesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMaintenances";

    public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, UpdatedMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly MaintenanceBusinessRules _maintenanceBusinessRules;

        public UpdateMaintenanceCommandHandler(IMapper mapper, IMaintenanceRepository maintenanceRepository,
                                         MaintenanceBusinessRules maintenanceBusinessRules)
        {
            _mapper = mapper;
            _maintenanceRepository = maintenanceRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
        }

        public async Task<UpdatedMaintenanceResponse> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            Maintenance? maintenance = await _maintenanceRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _maintenanceBusinessRules.MaintenanceShouldExistWhenSelected(maintenance);
            maintenance = _mapper.Map(request, maintenance);

            await _maintenanceRepository.UpdateAsync(maintenance!);

            UpdatedMaintenanceResponse response = _mapper.Map<UpdatedMaintenanceResponse>(maintenance);
            return response;
        }
    }
}