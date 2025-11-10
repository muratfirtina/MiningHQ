using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserRoles.Commands.AssignRole;

public class AssignRoleToUserCommand : IRequest<AssignedRoleToUserResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, AssignedRoleToUserResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public AssignRoleToUserCommandHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<AssignedRoleToUserResponse> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            UserRole userRole = new(userId: request.UserId, roleId: request.RoleId);
            UserRole assignedUserRole = await _userRoleRepository.AddAsync(userRole);
            AssignedRoleToUserResponse response = _mapper.Map<AssignedRoleToUserResponse>(assignedUserRole);
            return response;
        }
    }
}
