using Core.Persistence.Repositories;

namespace Domain.Entities;

public class MachineType:Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<Brand> Brands { get; set; }
    public ICollection<Machine> Machines { get; set; }
}