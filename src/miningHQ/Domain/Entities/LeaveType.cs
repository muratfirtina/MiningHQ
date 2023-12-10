using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LeaveType:Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<EmployeeLeaveUsage>? EmployeeLeaveUsages { get; set; }
    public ICollection<EntitledLeave>? EntitledLeaves { get; set; }
}