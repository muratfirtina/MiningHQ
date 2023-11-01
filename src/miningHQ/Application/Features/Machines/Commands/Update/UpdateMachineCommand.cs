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

namespace Application.Features.Machines.Commands.Update;

public class UpdateMachineCommand : IRequest<UpdatedMachineResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public Model? Model { get; set; }
    public Guid QuarryId { get; set; }
    public Quarry? Quarry { get; set; }
    public string SerialNumber { get; set; }
    public string? Name { get; set; }
    public Guid MachineTypeId { get; set; }
    public MachineType MachineType { get; set; }

    public string[] Roles => new[] { Admin, Write, MachinesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMachines";

    public class UpdateMachineCommandHandler : IRequestHandler<UpdateMachineCommand, UpdatedMachineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineRepository _machineRepository;
        private readonly MachineBusinessRules _machineBusinessRules;

        public UpdateMachineCommandHandler(IMapper mapper, IMachineRepository machineRepository,
                                         MachineBusinessRules machineBusinessRules)
        {
            _mapper = mapper;
            _machineRepository = machineRepository;
            _machineBusinessRules = machineBusinessRules;
        }

        public async Task<UpdatedMachineResponse> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
        {
            Machine? machine = await _machineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _machineBusinessRules.MachineShouldExistWhenSelected(machine);
            machine = _mapper.Map(request, machine);

            await _machineRepository.UpdateAsync(machine!);

            UpdatedMachineResponse response = _mapper.Map<UpdatedMachineResponse>(machine);
            return response;
        }
    }
}