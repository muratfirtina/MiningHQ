using Core.Application.Responses;

namespace Application.Features.DailyEntries.Queries.GetMachinesForDailyEntry;

public class GetMachinesForDailyEntryResponse : IResponse
{
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
    public string? MachineTypeName { get; set; }
    public string? QuarryName { get; set; }
    public string SerialNumber { get; set; }
    public string? CurrentOperatorName { get; set; }
    
    // Current data
    public int CurrentTotalHours { get; set; }
    public DateTime? LastEntryDate { get; set; }
    
    // Entry fields (will be filled by user)
    public int NewTotalHours { get; set; }
    public double FuelConsumption { get; set; }
}
