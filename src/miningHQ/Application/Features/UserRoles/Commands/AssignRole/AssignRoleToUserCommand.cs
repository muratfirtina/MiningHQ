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
            // Check if user already has this role to prevent duplicate key violation
            UserRole? existingUserRole = await _userRoleRepository.GetAsync(
                predicate: ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            if (existingUserRole != null)
            {
                // User already has this role, return the existing assignment
                AssignedRoleToUserResponse existingResponse = _mapper.Map<AssignedRoleToUserResponse>(existingUserRole);
                return existingResponse;
            }

            // User doesn't have this role yet, assign it
            UserRole userRole = new(userId: request.UserId, roleId: request.RoleId);
            UserRole assignedUserRole = await _userRoleRepository.AddAsync(userRole);
            AssignedRoleToUserResponse response = _mapper.Map<AssignedRoleToUserResponse>(assignedUserRole);
            return response;
        }
    }
}
