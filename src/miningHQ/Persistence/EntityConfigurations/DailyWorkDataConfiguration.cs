using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DailyWorkDataConfiguration : IEntityTypeConfiguration<DailyWorkData>
{
    public void Configure(EntityTypeBuilder<DailyWorkData> builder)
    {
        builder.ToTable("DailyWorkDatas").HasKey(dwd => dwd.Id);

        builder.Property(dwd => dwd.Id).HasColumnName("Id").IsRequired();
        builder.Property(dwd => dwd.Date).HasColumnName("Date");
        builder.Property(dwd => dwd.WorkingHoursOrKm).HasColumnName("WorkingHoursOrKm");
        builder.Property(dwd => dwd.MachineId).HasColumnName("MachineId");
        builder.Property(dwd => dwd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dwd => dwd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dwd => dwd.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasOne(dwd => dwd.Machine)
            .WithMany(m => m.DailyWorkDatas)
            .HasForeignKey(dwd => dwd.MachineId);

        builder.HasQueryFilter(dwd => !dwd.DeletedDate.HasValue);
    }
}