using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Maintenance:Entity<Guid>
{
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
    public Guid MaintenanceTypeId { get; set; }
    public MaintenanceType MaintenanceType { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int MachineWorkingTimeOrKilometer { get; set; } // Bakımı yapılan makinenin çalışma süresi veya kilometre bilgisi
    public string MaintenanceFirm { get; set; } // Bakımı yapan firma adı
    public int? NextMaintenanceHour { get; set; } // Bir sonraki bakım saati (makine saati olarak)
    public string? PartsChanged { get; set; } // Değiştirilen veya eklenen parçalar
    public string? OilsChanged { get; set; } // Değiştirilen yağlar
    
    // Bakım ile ilgili taranmış evrakların ve fotoğrafların olduğu entity
    public ICollection<MaintenanceFile> MaintenanceFiles { get; set; }
}
