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

namespace Application.Features.Machines.Commands.Create;

public class CreateMachineCommand : IRequest<CreatedMachineResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string ModelId { get; set; }
    public string QuarryId { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string MachineTypeId { get; set; }
    

    public string[] Roles => new[] { Admin, Write, MachinesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetMachines"};

    public class CreateMachineCommandHandler : IRequestHandler<CreateMachineCommand, CreatedMachineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineRepository _machineRepository;
        private readonly MachineBusinessRules _machineBusinessRules;

        public CreateMachineCommandHandler(IMapper mapper, IMachineRepository machineRepository,
                                         MachineBusinessRules machineBusinessRules)
        {
            _mapper = mapper;
            _machineRepository = machineRepository;
            _machineBusinessRules = machineBusinessRules;
        }

        public async Task<CreatedMachineResponse> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
        {
            Machine machine = _mapper.Map<Machine>(request);

            await _machineRepository.AddAsync(machine);

            CreatedMachineResponse response = _mapper.Map<CreatedMachineResponse>(machine);
            return response;
        }
    }
}