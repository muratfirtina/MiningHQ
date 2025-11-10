using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class RoleOperationClaim : Entity<Guid>
{
    public int RoleId { get; set; }
    public int OperationClaimId { get; set; }

    public virtual Role Role { get; set; } = null!;
    public virtual OperationClaim OperationClaim { get; set; } = null!;

    public RoleOperationClaim()
    {
    }

    public RoleOperationClaim(int roleId, int operationClaimId)
    {
        RoleId = roleId;
        OperationClaimId = operationClaimId;
    }

    public RoleOperationClaim(Guid id, int roleId, int operationClaimId)
        : base(id)
    {
        RoleId = roleId;
        OperationClaimId = operationClaimId;
    }
}
