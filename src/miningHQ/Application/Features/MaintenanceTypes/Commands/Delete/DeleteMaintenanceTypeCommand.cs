using Application.Features.MaintenanceTypes.Constants;
using Application.Features.MaintenanceTypes.Constants;
using Application.Features.MaintenanceTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.MaintenanceTypes.Constants.MaintenanceTypesOperationClaims;

namespace Application.Features.MaintenanceTypes.Commands.Delete;

public class DeleteMaintenanceTypeCommand : IRequest<DeletedMaintenanceTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenanceTypesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMaintenanceTypes";

    public class DeleteMaintenanceTypeCommandHandler : IRequestHandler<DeleteMaintenanceTypeCommand, DeletedMaintenanceTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceTypeRepository _maintenanceTypeRepository;
        private readonly MaintenanceTypeBusinessRules _maintenanceTypeBusinessRules;

        public DeleteMaintenanceTypeCommandHandler(IMapper mapper, IMaintenanceTypeRepository maintenanceTypeRepository,
                                         MaintenanceTypeBusinessRules maintenanceTypeBusinessRules)
        {
            _mapper = mapper;
            _maintenanceTypeRepository = maintenanceTypeRepository;
            _maintenanceTypeBusinessRules = maintenanceTypeBusinessRules;
        }

        public async Task<DeletedMaintenanceTypeResponse> Handle(DeleteMaintenanceTypeCommand request, CancellationToken cancellationToken)
        {
            MaintenanceType? maintenanceType = await _maintenanceTypeRepository.GetAsync(predicate: mt => mt.Id == request.Id, cancellationToken: cancellationToken);
            await _maintenanceTypeBusinessRules.MaintenanceTypeShouldExistWhenSelected(maintenanceType);

            await _maintenanceTypeRepository.DeleteAsync(maintenanceType!);

            DeletedMaintenanceTypeResponse response = _mapper.Map<DeletedMaintenanceTypeResponse>(maintenanceType);
            return response;
        }
    }
}