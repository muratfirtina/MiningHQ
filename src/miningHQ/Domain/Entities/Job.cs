using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Job:Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}