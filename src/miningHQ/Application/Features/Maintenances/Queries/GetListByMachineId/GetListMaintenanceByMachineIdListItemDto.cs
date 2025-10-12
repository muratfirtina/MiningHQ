namespace Application.Features.Maintenances.Queries.GetListByMachineId;

public class GetListMaintenanceByMachineIdListItemDto
{
    public Guid Id { get; set; }
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public Guid MaintenanceTypeId { get; set; }
    public string MaintenanceTypeName { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int MachineWorkingTimeOrKilometer { get; set; }
    public string MaintenanceFirm { get; set; }
    public int? NextMaintenanceHour { get; set; }
    public string? PartsChanged { get; set; }
    public string? OilsChanged { get; set; }
    public int FileCount { get; set; }
}
