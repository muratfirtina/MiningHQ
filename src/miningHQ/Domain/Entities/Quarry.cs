using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Quarry:Entity<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    
    // Koordinat bilgileri
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
    
    // Maden Mühendisi
    public Guid? MiningEngineerId { get; set; }
    public Employee? MiningEngineer { get; set; }
    
    // İlişkiler
    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Machine>? Machines { get; set; }
    public ICollection<QuarryFile>? QuarryFiles { get; set; }
    public ICollection<QuarryProduction>? QuarryProductions { get; set; }
    
    public Quarry()
    {
        
    }
    
    public Quarry(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}