using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.RoleOperationClaims.Commands.RemoveClaim;

public class RemoveClaimFromRoleCommand : IRequest<RemovedClaimFromRoleResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class RemoveClaimFromRoleCommandHandler : IRequestHandler<RemoveClaimFromRoleCommand, RemovedClaimFromRoleResponse>
    {
        private readonly IRoleOperationClaimRepository _roleOperationClaimRepository;
        private readonly IMapper _mapper;

        public RemoveClaimFromRoleCommandHandler(IRoleOperationClaimRepository roleOperationClaimRepository, IMapper mapper)
        {
            _roleOperationClaimRepository = roleOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<RemovedClaimFromRoleResponse> Handle(RemoveClaimFromRoleCommand request, CancellationToken cancellationToken)
        {
            RoleOperationClaim? roleOperationClaim = await _roleOperationClaimRepository.GetAsync(
                predicate: roc => roc.Id == request.Id,
                cancellationToken: cancellationToken
            );

            if (roleOperationClaim == null)
                throw new Exception("RoleOperationClaim not found");

            RoleOperationClaim deletedRoleOperationClaim = await _roleOperationClaimRepository.DeleteAsync(roleOperationClaim);
            RemovedClaimFromRoleResponse response = _mapper.Map<RemovedClaimFromRoleResponse>(deletedRoleOperationClaim);
            return response;
        }
    }
}
