namespace Domain.Entities;

public class MaintenanceFile:File
{
    ICollection<Maintenance> Maintenances { get; set; }
}