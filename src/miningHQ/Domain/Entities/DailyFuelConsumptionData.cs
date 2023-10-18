using Core.Persistence.Repositories;

namespace Domain.Entities;

public class DailyFuelConsumptionData:Entity<Guid>
{
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public Guid MachineId { get; set; }
    public Machine? Machine { get; set; }
}