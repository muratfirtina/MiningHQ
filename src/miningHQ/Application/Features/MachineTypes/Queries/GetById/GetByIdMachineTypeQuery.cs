using Application.Features.MachineTypes.Constants;
using Application.Features.MachineTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.MachineTypes.Constants.MachineTypesOperationClaims;

namespace Application.Features.MachineTypes.Queries.GetById;

public class GetByIdMachineTypeQuery : IRequest<GetByIdMachineTypeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMachineTypeQueryHandler : IRequestHandler<GetByIdMachineTypeQuery, GetByIdMachineTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly MachineTypeBusinessRules _machineTypeBusinessRules;

        public GetByIdMachineTypeQueryHandler(IMapper mapper, IMachineTypeRepository machineTypeRepository, MachineTypeBusinessRules machineTypeBusinessRules)
        {
            _mapper = mapper;
            _machineTypeRepository = machineTypeRepository;
            _machineTypeBusinessRules = machineTypeBusinessRules;
        }

        public async Task<GetByIdMachineTypeResponse> Handle(GetByIdMachineTypeQuery request, CancellationToken cancellationToken)
        {
            MachineType? machineType = await _machineTypeRepository.GetAsync(predicate: mt => mt.Id == request.Id, cancellationToken: cancellationToken);
            await _machineTypeBusinessRules.MachineTypeShouldExistWhenSelected(machineType);

            GetByIdMachineTypeResponse response = _mapper.Map<GetByIdMachineTypeResponse>(machineType);
            return response;
        }
    }
}