namespace Domain.Entities;

public class MaintenanceFile:File
{
    public ICollection<Maintenance> Maintenances { get; set; }
}