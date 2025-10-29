using Core.Application.Responses;

namespace Application.Features.Quarries.Commands.Create;

public class CreatedQuarryResponse : IResponse
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
}
