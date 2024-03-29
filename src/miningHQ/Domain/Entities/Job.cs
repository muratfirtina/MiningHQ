using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Job:Entity<Guid>
{
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}