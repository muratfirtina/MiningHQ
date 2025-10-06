using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Machines.Queries.GetList;

public class GetListMachineListItemDto : IDto
{
    public string Id { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string QuarryName { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string MachineTypeName { get; set; }
    
    // IDs for easier access
    public string? BrandId { get; set; }
    public string? ModelId { get; set; }
    public string? MachineTypeId { get; set; }
    public string? QuarryId { get; set; }
    
    // Additional fields
    public DateTime? PurchaseDate { get; set; }
    public string? Description { get; set; }
}