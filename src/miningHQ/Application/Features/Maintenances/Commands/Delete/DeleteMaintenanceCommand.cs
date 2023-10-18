using Application.Features.Maintenances.Constants;
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

namespace Application.Features.Maintenances.Commands.Delete;

public class DeleteMaintenanceCommand : IRequest<DeletedMaintenanceResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenancesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMaintenances";

    public class DeleteMaintenanceCommandHandler : IRequestHandler<DeleteMaintenanceCommand, DeletedMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly MaintenanceBusinessRules _maintenanceBusinessRules;

        public DeleteMaintenanceCommandHandler(IMapper mapper, IMaintenanceRepository maintenanceRepository,
                                         MaintenanceBusinessRules maintenanceBusinessRules)
        {
            _mapper = mapper;
            _maintenanceRepository = maintenanceRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
        }

        public async Task<DeletedMaintenanceResponse> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            Maintenance? maintenance = await _maintenanceRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _maintenanceBusinessRules.MaintenanceShouldExistWhenSelected(maintenance);

            await _maintenanceRepository.DeleteAsync(maintenance!);

            DeletedMaintenanceResponse response = _mapper.Map<DeletedMaintenanceResponse>(maintenance);
            return response;
        }
    }
}