namespace Application.Features.Roles.Commands.Create;

public class CreatedRoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
