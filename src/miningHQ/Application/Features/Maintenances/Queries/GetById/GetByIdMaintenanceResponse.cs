using Core.Application.Responses;

namespace Application.Features.Maintenances.Queries.GetById;

public class GetByIdMaintenanceResponse : IResponse
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
    public List<MaintenanceFileDto> MaintenanceFiles { get; set; }
}

public class MaintenanceFileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Category { get; set; }
    public string Storage { get; set; }
}
