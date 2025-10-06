using Core.Application.Responses;

namespace Application.Features.Machines.Queries.GetById;

public class GetByIdMachineResponse : IResponse
{
    public Guid Id { get; set; }
    
    // Model Info
    public Guid ModelId { get; set; }
    public string? ModelName { get; set; }
    
    // Brand Info
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    
    // Quarry Info
    public Guid QuarryId { get; set; }
    public string? QuarryName { get; set; }
    
    // Machine Type Info
    public Guid MachineTypeId { get; set; }
    public string? MachineTypeName { get; set; }
    
    // Machine Details
    public string SerialNumber { get; set; }
    public string? Name { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? StartWorkDate { get; set; }
    public double? InitialWorkingHoursOrKm { get; set; }
    public string? Description { get; set; }
    
    // Working Hours
    public decimal CurrentWorkingHours { get; set; }
    
    // Current Operator
    public Guid? CurrentOperatorId { get; set; }
    public string? CurrentOperatorName { get; set; }
}