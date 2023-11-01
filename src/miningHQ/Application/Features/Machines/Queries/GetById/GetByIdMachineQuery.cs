using Application.Features.Machines.Constants;
using Application.Features.Machines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Queries.GetById;

public class GetByIdMachineQuery : IRequest<GetByIdMachineResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMachineQueryHandler : IRequestHandler<GetByIdMachineQuery, GetByIdMachineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineRepository _machineRepository;
        private readonly MachineBusinessRules _machineBusinessRules;

        public GetByIdMachineQueryHandler(IMapper mapper, IMachineRepository machineRepository, MachineBusinessRules machineBusinessRules)
        {
            _mapper = mapper;
            _machineRepository = machineRepository;
            _machineBusinessRules = machineBusinessRules;
        }

        public async Task<GetByIdMachineResponse> Handle(GetByIdMachineQuery request, CancellationToken cancellationToken)
        {
            Machine? machine = await _machineRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _machineBusinessRules.MachineShouldExistWhenSelected(machine);

            GetByIdMachineResponse response = _mapper.Map<GetByIdMachineResponse>(machine);
            return response;
        }
    }
}