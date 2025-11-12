namespace Application.Features.Roles.Commands.Update;

public class UpdatedRoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
