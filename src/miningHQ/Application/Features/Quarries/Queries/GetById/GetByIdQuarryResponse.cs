using Core.Application.Responses;

namespace Application.Features.Quarries.Queries.GetById;

public class GetByIdQuarryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    
    // Konum bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }
    public double? UtmNorthing { get; set; }
    public double? Altitude { get; set; }
    public string? Pafta { get; set; }
    
    // GPS koordinatları (otomatik dönüştürülür)
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
    
    public Guid? MiningEngineerId { get; set; }
    public MiningEngineerDto? MiningEngineer { get; set; }
    public List<EmployeeDto>? Employees { get; set; }
    public List<MachineDto>? Machines { get; set; }
    public List<QuarryFileDto>? QuarryFiles { get; set; }
    public List<QuarryProductionDto>? QuarryProductions { get; set; }
}

public class MiningEngineerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
}

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? JobName { get; set; }
    public string? DepartmentName { get; set; }
    public string? Phone { get; set; }
}

public class MachineDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string SerialNumber { get; set; }
    public string? MachineTypeName { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
}

public class QuarryFileDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
}

public class QuarryProductionDto
{
    public Guid Id { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public decimal ProductionAmount { get; set; }
    public string? ProductionUnit { get; set; }
    public decimal StockAmount { get; set; }
    public string? StockUnit { get; set; }
    public decimal SalesAmount { get; set; }
    public string? SalesUnit { get; set; }
    public string? Notes { get; set; }
    
    // Konum bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }
    public double? UtmNorthing { get; set; }
    public double? Altitude { get; set; }
    public string? Pafta { get; set; }
    
    // GPS koordinatları
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
}
