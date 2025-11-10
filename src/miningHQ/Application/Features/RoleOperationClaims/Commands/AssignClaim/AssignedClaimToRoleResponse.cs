namespace Application.Features.RoleOperationClaims.Commands.AssignClaim;

public class AssignedClaimToRoleResponse
{
    public Guid Id { get; set; }
    public int RoleId { get; set; }
    public int OperationClaimId { get; set; }
}
