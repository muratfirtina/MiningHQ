namespace Application.Features.RoleOperationClaims.Queries.GetByRoleId;

public class GetRoleClaimsResponse
{
    public Guid Id { get; set; }
    public int OperationClaimId { get; set; }
    public string OperationClaimName { get; set; } = string.Empty;
}
