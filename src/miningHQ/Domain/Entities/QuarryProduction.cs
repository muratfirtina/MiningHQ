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
