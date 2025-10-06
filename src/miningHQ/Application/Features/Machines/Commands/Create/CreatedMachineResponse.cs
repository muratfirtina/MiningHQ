using Core.Application.Responses;

namespace Application.Features.Machines.Commands.Create;

public class CreatedMachineResponse : IResponse
{
    public string Id { get; set; }
    
    // Model Info
    public string? ModelId { get; set; }
    public string? ModelName { get; set; }
    
    // Brand Info
    public string? BrandId { get; set; }
    public string? BrandName { get; set; }
    
    // Quarry Info
    public string? QuarryId { get; set; }
    public string? QuarryName { get; set; }
    
    // Machine Type Info
    public string? MachineTypeId { get; set; }
    public string? MachineTypeName { get; set; }
    
    // Machine Details
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public string? Description { get; set; }
}