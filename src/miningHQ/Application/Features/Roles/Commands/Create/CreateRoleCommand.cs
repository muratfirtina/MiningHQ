using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Constants;
using MediatR;

namespace Application.Features.Roles.Commands.Create;

public class CreateRoleCommand : IRequest<CreatedRoleResponse>, ISecuredRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreatedRoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<CreatedRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = _mapper.Map<Role>(request);
            Role createdRole = await _roleRepository.AddAsync(role);
            CreatedRoleResponse response = _mapper.Map<CreatedRoleResponse>(createdRole);
            return response;
        }
    }
}
