using Core.Application.Responses;

namespace Application.Features.Machines.Queries.GetMachineStats;

public class GetMachineStatsResponse : IResponse
{
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public int TotalWorkDays { get; set; }
    public decimal TotalWorkHours { get; set; }
    public decimal TotalFuelUsed { get; set; }
    public decimal AverageFuelConsumptionPerHour { get; set; }
    public int MaintenanceCount { get; set; }
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextScheduledMaintenance { get; set; }
    public decimal TotalProductionAmount { get; set; }
}
