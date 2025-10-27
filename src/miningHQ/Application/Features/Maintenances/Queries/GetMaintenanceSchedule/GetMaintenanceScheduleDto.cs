namespace Application.Features.Maintenances.Queries.GetMaintenanceSchedule;

public class GetMaintenanceScheduleDto
{
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public string SerialNumber { get; set; }
    public string MachineTypeName { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string QuarryName { get; set; }
    
    // Son bakım bilgileri
    public DateTime? LastMaintenanceDate { get; set; }
    public int? LastMaintenanceHourOrKm { get; set; }
    public string? LastMaintenanceType { get; set; }
    public string? LastMaintenanceFirm { get; set; }
    
    // Gelecek bakım bilgileri
    public int? NextMaintenanceHour { get; set; }
    public int? CurrentWorkingHourOrKm { get; set; } // Şu anki çalışma saati/km (son daily work data'dan)
    public int? RemainingHourOrKm { get; set; } // Kalan saat/km
    
    // Durum bilgisi
    public string MaintenanceStatus { get; set; } // "Yaklaşıyor", "Gecikmiş", "Normal" gibi
}
