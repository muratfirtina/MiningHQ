using Core.Application.Dtos;

namespace Application.Features.Quarries.Queries.GetList;

public class GetListQuarryListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
    public string? MiningEngineerId { get; set; }
    
    // Navigation properties for display
    public MiningEngineerListDto? MiningEngineer { get; set; }
    public int EmployeeCount { get; set; }
    public int MachineCount { get; set; }
}

public class MiningEngineerListDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
}
