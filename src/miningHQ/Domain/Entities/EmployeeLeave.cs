using Core.Persistence.Repositories;

namespace Domain.Entities;

public class EmployeeLeave:Entity<Guid>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; } // Hangi çalışana ait olduğunu belirten ilişki
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; } // Toplam izin gün sayısı
    public ICollection<LeaveUsage> LeaveUsages { get; set; } = new List<LeaveUsage>(); // İzin kullanım bilgileri
}