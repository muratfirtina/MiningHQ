using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoleOperationClaims.Queries.GetByRoleId;

public class GetRoleClaimsQuery : IRequest<List<GetRoleClaimsResponse>>, ISecuredRequest
{
    public int RoleId { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class GetRoleClaimsQueryHandler : IRequestHandler<GetRoleClaimsQuery, List<GetRoleClaimsResponse>>
    {
        private readonly IRoleOperationClaimRepository _roleOperationClaimRepository;

        public GetRoleClaimsQueryHandler(IRoleOperationClaimRepository roleOperationClaimRepository)
        {
            _roleOperationClaimRepository = roleOperationClaimRepository;
        }

        public async Task<List<GetRoleClaimsResponse>> Handle(GetRoleClaimsQuery request, CancellationToken cancellationToken)
        {
            var roleClaims = await _roleOperationClaimRepository
                .Query()
                .Where(roc => roc.RoleId == request.RoleId)
                .Include(roc => roc.OperationClaim)
                .Select(roc => new GetRoleClaimsResponse
                {
                    Id = roc.Id,
                    OperationClaimId = roc.OperationClaimId,
                    OperationClaimName = roc.OperationClaim.Name
                })
                .ToListAsync(cancellationToken);

            return roleClaims;
        }
    }
}
