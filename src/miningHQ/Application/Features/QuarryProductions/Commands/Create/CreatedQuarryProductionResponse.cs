using Core.Application.Responses;

namespace Application.Features.QuarryProductions.Commands.Create;

public class CreatedQuarryProductionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid QuarryId { get; set; }
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
    
    // GPS koordinatları (otomatik dönüştürülür)
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
}
