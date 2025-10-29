using Core.Persistence.Repositories;

namespace Domain.Entities;

public class QuarryProduction : Entity<Guid>
{
    public Guid QuarryId { get; set; }
    public Quarry? Quarry { get; set; }
    
    // Haftalık üretim verileri
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    
    // Üretim miktarı (ton, m³ gibi birimler description'da belirtilir)
    public decimal ProductionAmount { get; set; }
    public string? ProductionUnit { get; set; } // "ton", "m³", "kg" vb.
    
    // Stok miktarı
    public decimal StockAmount { get; set; }
    public string? StockUnit { get; set; }
    
    // Satış miktarı
    public decimal SalesAmount { get; set; }
    public string? SalesUnit { get; set; }
    
    // Konum bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }  // UTM X (Doğu)
    public double? UtmNorthing { get; set; } // UTM Y (Kuzey)
    public double? Altitude { get; set; }    // Yükseklik (metre)
    public string? Pafta { get; set; }       // Pafta bilgisi
    
    // Dönüştürülmüş WGS84 koordinatları (Google Maps için)
    public double? Latitude { get; set; }    // Enlem
    public double? Longitude { get; set; }   // Boylam
    public string? CoordinateDescription { get; set; }
    
    // Ek bilgiler
    public string? Notes { get; set; }
    
    public QuarryProduction()
    {
        
    }
    
    public QuarryProduction(Guid id, Guid quarryId, DateTime weekStartDate, DateTime weekEndDate) : this()
    {
        Id = id;
        QuarryId = quarryId;
        WeekStartDate = weekStartDate;
        WeekEndDate = weekEndDate;
    }
}
