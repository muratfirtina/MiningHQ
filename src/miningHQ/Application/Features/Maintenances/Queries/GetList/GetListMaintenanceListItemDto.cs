using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Maintenances.Queries.GetList;

public class GetListMaintenanceListItemDto : IDto
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