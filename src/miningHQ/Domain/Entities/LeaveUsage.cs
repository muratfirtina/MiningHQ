using Core.Persistence.Repositories;

namespace Domain.Entities;

public class LeaveUsage:Entity<Guid>
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; } // Hangi izinle ilişkili olduğunu belirten ilişki
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; } // İzin kullanım tarihi
    public DateTime? ReturnDate { get; set; } // İzin dönüş tarihi (isteğe bağlı olarak null olabilir)
}