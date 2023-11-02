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

namespace Application.Features.MachineTypes.Commands.Create;

public class CreateMachineTypeCommand : IRequest<CreatedMachineTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, MachineTypesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] {"GetMachineTypes"};

    public class CreateMachineTypeCommandHandler : IRequestHandler<CreateMachineTypeCommand, CreatedMachineTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly MachineTypeBusinessRules _machineTypeBusinessRules;

        public CreateMachineTypeCommandHandler(IMapper mapper, IMachineTypeRepository machineTypeRepository,
                                         MachineTypeBusinessRules machineTypeBusinessRules)
        {
            _mapper = mapper;
            _machineTypeRepository = machineTypeRepository;
            _machineTypeBusinessRules = machineTypeBusinessRules;
        }

        public async Task<CreatedMachineTypeResponse> Handle(CreateMachineTypeCommand request, CancellationToken cancellationToken)
        {
            MachineType machineType = _mapper.Map<MachineType>(request);

            await _machineTypeRepository.AddAsync(machineType);

            CreatedMachineTypeResponse response = _mapper.Map<CreatedMachineTypeResponse>(machineType);
            return response;
        }
    }
}