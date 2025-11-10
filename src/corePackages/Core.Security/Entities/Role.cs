using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class Role : Entity<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
    public virtual ICollection<RoleOperationClaim> RoleOperationClaims { get; set; } = null!;

    public Role()
    {
        Name = string.Empty;
    }

    public Role(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public Role(int id, string name, string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }
}
