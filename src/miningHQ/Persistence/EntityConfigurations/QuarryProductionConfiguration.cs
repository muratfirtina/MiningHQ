using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuarryProductionConfiguration : IEntityTypeConfiguration<QuarryProduction>
{
    public void Configure(EntityTypeBuilder<QuarryProduction> builder)
    {
        builder.ToTable("QuarryProductions").HasKey(qp => qp.Id);

        builder.Property(qp => qp.Id).HasColumnName("Id").IsRequired();
        builder.Property(qp => qp.QuarryId).HasColumnName("QuarryId").IsRequired();
        builder.Property(qp => qp.WeekStartDate).HasColumnName("WeekStartDate").IsRequired();
        builder.Property(qp => qp.WeekEndDate).HasColumnName("WeekEndDate").IsRequired();
        builder.Property(qp => qp.ProductionAmount).HasColumnName("ProductionAmount").HasPrecision(18, 2);
        builder.Property(qp => qp.ProductionUnit).HasColumnName("ProductionUnit").HasMaxLength(20);
        builder.Property(qp => qp.StockAmount).HasColumnName("StockAmount").HasPrecision(18, 2);
        builder.Property(qp => qp.StockUnit).HasColumnName("StockUnit").HasMaxLength(20);
        builder.Property(qp => qp.SalesAmount).HasColumnName("SalesAmount").HasPrecision(18, 2);
        builder.Property(qp => qp.SalesUnit).HasColumnName("SalesUnit").HasMaxLength(20);
        builder.Property(qp => qp.Notes).HasColumnName("Notes").HasMaxLength(500);
        builder.Property(qp => qp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(qp => qp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(qp => qp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(qp => qp.Quarry)
            .WithMany(q => q.QuarryProductions)
            .HasForeignKey(qp => qp.QuarryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(qp => !qp.DeletedDate.HasValue);
    }
}
