using Core.Persistence.Repositories;

namespace Domain.Entities;
public class Machine:Entity<Guid>
{
    public Guid ModelId { get; set; }
    public Model? Model { get; set; }
    public Guid QuarryId { get; set; }
    public Quarry? Quarry { get; set; }
    public string SerialNumber { get; set; }
    public string? Name { get; set; }
    
    public ICollection<Employee>? Employees { get; set; }
    
    public ICollection<DailyWorkData> DailyWorkDatas { get; set; }
    public ICollection<DailyFuelConsumptionData> DailyFuelConsumptionDatas { get; set; }
    
    
    public Guid MachineTypeId { get; set; } 
    public MachineType MachineType { get; set; }
    
    public ICollection<Maintenance>? Maintenances { get; set; }
    
    // Navigation properties for files
    public ICollection<MachineFile> MachineFiles { get; set; } = new List<MachineFile>();
    

    public Machine()
    {
        
    }
    
    public Machine(Guid id, Guid modelId, string serialNumber, string name)
    {
        Id = id;
        ModelId = modelId;
        SerialNumber = serialNumber;
        Name = name;
    }
    
}
