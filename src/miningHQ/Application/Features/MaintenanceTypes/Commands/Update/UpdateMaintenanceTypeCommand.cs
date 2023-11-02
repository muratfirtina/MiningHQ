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

namespace Application.Features.MaintenanceTypes.Commands.Update;

public class UpdateMaintenanceTypeCommand : IRequest<UpdatedMaintenanceTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenanceTypesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetMaintenanceTypes"};

    public class UpdateMaintenanceTypeCommandHandler : IRequestHandler<UpdateMaintenanceTypeCommand, UpdatedMaintenanceTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceTypeRepository _maintenanceTypeRepository;
        private readonly MaintenanceTypeBusinessRules _maintenanceTypeBusinessRules;

        public UpdateMaintenanceTypeCommandHandler(IMapper mapper, IMaintenanceTypeRepository maintenanceTypeRepository,
                                         MaintenanceTypeBusinessRules maintenanceTypeBusinessRules)
        {
            _mapper = mapper;
            _maintenanceTypeRepository = maintenanceTypeRepository;
            _maintenanceTypeBusinessRules = maintenanceTypeBusinessRules;
        }

        public async Task<UpdatedMaintenanceTypeResponse> Handle(UpdateMaintenanceTypeCommand request, CancellationToken cancellationToken)
        {
            MaintenanceType? maintenanceType = await _maintenanceTypeRepository.GetAsync(predicate: mt => mt.Id == request.Id, cancellationToken: cancellationToken);
            await _maintenanceTypeBusinessRules.MaintenanceTypeShouldExistWhenSelected(maintenanceType);
            maintenanceType = _mapper.Map(request, maintenanceType);

            await _maintenanceTypeRepository.UpdateAsync(maintenanceType!);

            UpdatedMaintenanceTypeResponse response = _mapper.Map<UpdatedMaintenanceTypeResponse>(maintenanceType);
            return response;
        }
    }
}