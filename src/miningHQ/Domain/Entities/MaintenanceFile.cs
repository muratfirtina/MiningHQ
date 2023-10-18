namespace Domain.Entities;

public class MaintenanceFile:File
{
    public Guid MaintenanceId { get; set; }
    public Maintenance? Maintenance { get; set; }
}