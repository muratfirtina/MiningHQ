using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserRoles.Commands.RemoveRole;

public class RemoveRoleFromUserCommand : IRequest<RemovedRoleFromUserResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, RemovedRoleFromUserResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public RemoveRoleFromUserCommandHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<RemovedRoleFromUserResponse> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            UserRole? userRole = await _userRoleRepository.GetAsync(
                predicate: ur => ur.Id == request.Id,
                cancellationToken: cancellationToken
            );

            if (userRole == null)
                throw new Exception("UserRole not found");

            UserRole deletedUserRole = await _userRoleRepository.DeleteAsync(userRole);
            RemovedRoleFromUserResponse response = _mapper.Map<RemovedRoleFromUserResponse>(deletedUserRole);
            return response;
        }
    }
}
