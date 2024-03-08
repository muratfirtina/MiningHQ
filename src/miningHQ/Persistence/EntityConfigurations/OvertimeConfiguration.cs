using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OvertimeConfiguration : IEntityTypeConfiguration<Overtime>
{
    public void Configure(EntityTypeBuilder<Overtime> builder)
    {
        builder.ToTable("Overtimes").HasKey(o => o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(o => o.OvertimeDate).HasColumnName("OvertimeDate");
        builder.Property(o => o.OvertimeHours).HasColumnName("OvertimeHours");
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}