using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Maintenances.Commands.Update;

public class UpdatedMaintenanceResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
    public Guid MaintenanceTypeId { get; set; }
    public MaintenanceType MaintenanceType { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int MachineWorkingTimeOrKilometer { get; set; }
    public string MaintenanceFirm { get; set; }
}