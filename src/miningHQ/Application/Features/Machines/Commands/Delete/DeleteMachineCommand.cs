using Application.Features.Machines.Constants;
using Application.Features.Machines.Constants;
using Application.Features.Machines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Commands.Delete;

public class DeleteMachineCommand : IRequest<DeletedMachineResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, MachinesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMachines";

    public class DeleteMachineCommandHandler : IRequestHandler<DeleteMachineCommand, DeletedMachineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineRepository _machineRepository;
        private readonly MachineBusinessRules _machineBusinessRules;

        public DeleteMachineCommandHandler(IMapper mapper, IMachineRepository machineRepository,
                                         MachineBusinessRules machineBusinessRules)
        {
            _mapper = mapper;
            _machineRepository = machineRepository;
            _machineBusinessRules = machineBusinessRules;
        }

        public async Task<DeletedMachineResponse> Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
        {
            Machine? machine = await _machineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _machineBusinessRules.MachineShouldExistWhenSelected(machine);

            await _machineRepository.DeleteAsync(machine!);

            DeletedMachineResponse response = _mapper.Map<DeletedMachineResponse>(machine);
            return response;
        }
    }
}