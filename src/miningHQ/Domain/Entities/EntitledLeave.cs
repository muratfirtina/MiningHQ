using Core.Persistence.Repositories;

namespace Domain.Entities;

public class EntitledLeave:Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime? EntitledDate { get; set; }
    public int? EntitledDays { get; set; }
}