namespace Application.Features.UserRoles.Commands.AssignRole;

public class AssignedRoleToUserResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
