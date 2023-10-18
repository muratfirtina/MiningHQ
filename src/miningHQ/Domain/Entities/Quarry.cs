using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Quarry:Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Machine>? Machines { get; set; }
}