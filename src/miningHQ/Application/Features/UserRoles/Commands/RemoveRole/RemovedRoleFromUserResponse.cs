namespace Application.Features.UserRoles.Commands.RemoveRole;

public class RemovedRoleFromUserResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
