using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeLeaveConfiguration : IEntityTypeConfiguration<EmployeeLeave>
{
    public void Configure(EntityTypeBuilder<EmployeeLeave> builder)
    {
        builder.ToTable("EmployeeLeaves").HasKey(el => el.Id);

        builder.Property(el => el.Id).HasColumnName("Id").IsRequired();
        builder.Property(el => el.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(el => el.TotalLeaveDays).HasColumnName("TotalLeaveDays");
        builder.Property(el => el.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(el => el.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(el => el.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasOne(el => el.Employee)
            .WithMany(e => e.EmployeeLeaves)
            .HasForeignKey(el => el.EmployeeId);

        builder.HasQueryFilter(el => !el.DeletedDate.HasValue);
    }
}