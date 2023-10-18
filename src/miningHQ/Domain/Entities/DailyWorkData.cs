using Core.Persistence.Repositories;

namespace Domain.Entities;

public class DailyWorkData:Entity<Guid>
{
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine? Machine { get; set; }
}