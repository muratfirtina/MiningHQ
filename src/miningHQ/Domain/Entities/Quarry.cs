using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Quarry:Entity<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    
    // Koordinat bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }  // UTM X (Doğu)
    public double? UtmNorthing { get; set; } // UTM Y (Kuzey)
    public double? Altitude { get; set; }    // Yükseklik (metre)
    public string? Pafta { get; set; }       // Pafta bilgisi
    
    // Dönüştürülmüş WGS84 koordinatları (Google Maps için)
    public double? Latitude { get; set; }    // Enlem
    public double? Longitude { get; set; }   // Boylam
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
