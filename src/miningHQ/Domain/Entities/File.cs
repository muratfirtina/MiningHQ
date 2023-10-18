using Core.Persistence.Repositories;

namespace Domain.Entities;

public class File:Entity<Guid>
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
    
}