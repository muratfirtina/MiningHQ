namespace Application.Features.QuarryModerators.Commands.Create;

public class CreatedQuarryModeratorResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuarryId { get; set; }
    public DateTime CreatedDate { get; set; }
}
