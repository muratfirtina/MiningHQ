using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Department : Entity<Guid>
{
    public string Name { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Job>? Jobs { get; set; }
    
    
}