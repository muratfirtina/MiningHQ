using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Constants;
using MediatR;

namespace Application.Features.Roles.Commands.Update;

public class UpdateRoleCommand : IRequest<UpdatedRoleResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdatedRoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _roleRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
            );

            if (role == null)
                throw new Exception("Role not found");

            role = _mapper.Map(request, role);
            Role updatedRole = await _roleRepository.UpdateAsync(role);
            UpdatedRoleResponse response = _mapper.Map<UpdatedRoleResponse>(updatedRole);
            return response;
        }
    }
}
