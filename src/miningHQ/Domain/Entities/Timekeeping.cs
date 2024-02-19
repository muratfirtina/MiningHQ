using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Timekeeping : Entity<Guid>
{
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public TimekeepingStatus Status { get; set; }
}