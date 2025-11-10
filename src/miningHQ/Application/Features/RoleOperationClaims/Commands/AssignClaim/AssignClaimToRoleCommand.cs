using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.RoleOperationClaims.Commands.AssignClaim;

public class AssignClaimToRoleCommand : IRequest<AssignedClaimToRoleResponse>, ISecuredRequest
{
    public int RoleId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class AssignClaimToRoleCommandHandler : IRequestHandler<AssignClaimToRoleCommand, AssignedClaimToRoleResponse>
    {
        private readonly IRoleOperationClaimRepository _roleOperationClaimRepository;
        private readonly IMapper _mapper;

        public AssignClaimToRoleCommandHandler(IRoleOperationClaimRepository roleOperationClaimRepository, IMapper mapper)
        {
            _roleOperationClaimRepository = roleOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<AssignedClaimToRoleResponse> Handle(AssignClaimToRoleCommand request, CancellationToken cancellationToken)
        {
            RoleOperationClaim roleOperationClaim = new(roleId: request.RoleId, operationClaimId: request.OperationClaimId);
            RoleOperationClaim assignedRoleOperationClaim = await _roleOperationClaimRepository.AddAsync(roleOperationClaim);
            AssignedClaimToRoleResponse response = _mapper.Map<AssignedClaimToRoleResponse>(assignedRoleOperationClaim);
            return response;
        }
    }
}
