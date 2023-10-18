using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("Maintenances").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.MachineId).HasColumnName("MachineId");
        builder.Property(m => m.MaintenanceTypeId).HasColumnName("MaintenanceTypeId");
        builder.Property(m => m.Description).HasColumnName("Description");
        builder.Property(m => m.MaintenanceDate).HasColumnName("MaintenanceDate");
        builder.Property(m => m.MachineWorkingTimeOrKilometer).HasColumnName("MachineWorkingTimeOrKilometer");
        builder.Property(m => m.MaintenanceFirm).HasColumnName("MaintenanceFirm");
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasOne(m => m.Machine)
            .WithMany(m => m.Maintenances)
            .HasForeignKey(m => m.MachineId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(m => m.MaintenanceType)
            .WithMany(m => m.Maintenances)
            .HasForeignKey(m => m.MaintenanceTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
    }
}