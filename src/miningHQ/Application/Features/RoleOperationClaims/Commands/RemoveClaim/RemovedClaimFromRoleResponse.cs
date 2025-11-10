namespace Application.Features.RoleOperationClaims.Commands.RemoveClaim;

public class RemovedClaimFromRoleResponse
{
    public Guid Id { get; set; }
    public int RoleId { get; set; }
    public int OperationClaimId { get; set; }
}
