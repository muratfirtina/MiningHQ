using Application.Features.MachineTypes.Constants;
using Application.Features.MachineTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.MachineTypes.Constants.MachineTypesOperationClaims;

namespace Application.Features.MachineTypes.Commands.Update;

public class UpdateMachineTypeCommand : IRequest<UpdatedMachineTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, MachineTypesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMachineTypes";

    public class UpdateMachineTypeCommandHandler : IRequestHandler<UpdateMachineTypeCommand, UpdatedMachineTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly MachineTypeBusinessRules _machineTypeBusinessRules;

        public UpdateMachineTypeCommandHandler(IMapper mapper, IMachineTypeRepository machineTypeRepository,
                                         MachineTypeBusinessRules machineTypeBusinessRules)
        {
            _mapper = mapper;
            _machineTypeRepository = machineTypeRepository;
            _machineTypeBusinessRules = machineTypeBusinessRules;
        }

        public async Task<UpdatedMachineTypeResponse> Handle(UpdateMachineTypeCommand request, CancellationToken cancellationToken)
        {
            MachineType? machineType = await _machineTypeRepository.GetAsync(predicate: mt => mt.Id == request.Id, cancellationToken: cancellationToken);
            await _machineTypeBusinessRules.MachineTypeShouldExistWhenSelected(machineType);
            machineType = _mapper.Map(request, machineType);

            await _machineTypeRepository.UpdateAsync(machineType!);

            UpdatedMachineTypeResponse response = _mapper.Map<UpdatedMachineTypeResponse>(machineType);
            return response;
        }
    }
}