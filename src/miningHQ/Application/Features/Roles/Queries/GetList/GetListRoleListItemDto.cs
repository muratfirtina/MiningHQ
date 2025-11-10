namespace Application.Features.Roles.Queries.GetList;

public class GetListRoleListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> Claims { get; set; } = new();
}
