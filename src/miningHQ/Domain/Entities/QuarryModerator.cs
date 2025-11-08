using Core.Security.Entities;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class QuarryModerator : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid QuarryId { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Quarry Quarry { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    
    public QuarryModerator()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
    
    public QuarryModerator(Guid userId, Guid quarryId, Guid createdBy) : this()
    {
        UserId = userId;
        QuarryId = quarryId;
        CreatedBy = createdBy;
    }
}
