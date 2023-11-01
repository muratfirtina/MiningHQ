using Application.Features.MachineTypes.Constants;
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

namespace Application.Features.MachineTypes.Commands.Delete;

public class DeleteMachineTypeCommand : IRequest<DeletedMachineTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MachineTypesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMachineTypes";

    public class DeleteMachineTypeCommandHandler : IRequestHandler<DeleteMachineTypeCommand, DeletedMachineTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly MachineTypeBusinessRules _machineTypeBusinessRules;

        public DeleteMachineTypeCommandHandler(IMapper mapper, IMachineTypeRepository machineTypeRepository,
                                         MachineTypeBusinessRules machineTypeBusinessRules)
        {
            _mapper = mapper;
            _machineTypeRepository = machineTypeRepository;
            _machineTypeBusinessRules = machineTypeBusinessRules;
        }

        public async Task<DeletedMachineTypeResponse> Handle(DeleteMachineTypeCommand request, CancellationToken cancellationToken)
        {
            MachineType? machineType = await _machineTypeRepository.GetAsync(predicate: mt => mt.Id == request.Id, cancellationToken: cancellationToken);
            await _machineTypeBusinessRules.MachineTypeShouldExistWhenSelected(machineType);

            await _machineTypeRepository.DeleteAsync(machineType!);

            DeletedMachineTypeResponse response = _mapper.Map<DeletedMachineTypeResponse>(machineType);
            return response;
        }
    }
}