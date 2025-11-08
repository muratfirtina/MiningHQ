namespace Application.Features.QuarryModerators.Commands.Delete;

public class DeletedQuarryModeratorResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuarryId { get; set; }
}
