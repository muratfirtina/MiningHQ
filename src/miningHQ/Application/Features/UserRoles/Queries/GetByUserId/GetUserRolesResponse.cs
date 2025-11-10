namespace Application.Features.UserRoles.Queries.GetByUserId;

public class GetUserRolesResponse
{
    public Guid Id { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? RoleDescription { get; set; }
}
