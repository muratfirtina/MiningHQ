using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MaintenanceFileConfiguration : IEntityTypeConfiguration<MaintenanceFile>
{
    public void Configure(EntityTypeBuilder<MaintenanceFile> builder)
    {
        builder.ToTable("MaintenanceFiles");

        // Many-to-Many relationship with Maintenances
        builder.HasMany(mf => mf.Maintenances)
               .WithMany(m => m.MaintenanceFiles)
               .UsingEntity(j => j.ToTable("MaintenanceFileMaintenance"));
    }
}
