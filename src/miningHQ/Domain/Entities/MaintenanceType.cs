using Core.Persistence.Repositories;

namespace Domain.Entities;

public class MaintenanceType:Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<Maintenance> Maintenances { get; set; }
}