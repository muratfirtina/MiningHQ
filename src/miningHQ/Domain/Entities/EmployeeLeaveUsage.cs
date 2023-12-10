using Core.Persistence.Repositories;

namespace Domain.Entities;

public class EmployeeLeaveUsage:Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public Employee? Employee { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? UsedDays { get; set; }
}