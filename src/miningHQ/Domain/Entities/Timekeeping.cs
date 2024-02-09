using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Timekeeping : Entity<Guid>
{
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public bool? Status { get; set; }
}