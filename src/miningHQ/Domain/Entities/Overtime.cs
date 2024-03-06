using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Overtime: Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
}