using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Model:Entity<Guid>
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<Machine> Machines { get; set; }
    
}