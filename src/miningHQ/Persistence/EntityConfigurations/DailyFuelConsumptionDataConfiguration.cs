using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DailyFuelConsumptionDataConfiguration : IEntityTypeConfiguration<DailyFuelConsumptionData>
{
    public void Configure(EntityTypeBuilder<DailyFuelConsumptionData> builder)
    {
        builder.ToTable("DailyFuelConsumptionDatas").HasKey(dfcd => dfcd.Id);

        builder.Property(dfcd => dfcd.Id).HasColumnName("Id").IsRequired();
        builder.Property(dfcd => dfcd.Date).HasColumnName("Date");
        builder.Property(dfcd => dfcd.FuelConsumption).HasColumnName("FuelConsumption");
        builder.Property(dfcd => dfcd.MachineId).HasColumnName("MachineId");
        builder.Property(dfcd => dfcd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dfcd => dfcd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dfcd => dfcd.DeletedDate).HasColumnName("DeletedDate");

        builder
            .HasOne(dfcd => dfcd.Machine)
            .WithMany(m => m.DailyFuelConsumptionDatas)
            .HasForeignKey(dfcd => dfcd.MachineId);

        builder.HasQueryFilter(dfcd => !dfcd.DeletedDate.HasValue);
    }
}