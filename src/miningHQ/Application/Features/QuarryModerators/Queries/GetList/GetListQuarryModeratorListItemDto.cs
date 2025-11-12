namespace Application.Features.QuarryModerators.Queries.GetList;

public class GetListQuarryModeratorListItemDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string UserFullName { get; set; } = string.Empty;
    public Guid QuarryId { get; set; }
    public string QuarryName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
