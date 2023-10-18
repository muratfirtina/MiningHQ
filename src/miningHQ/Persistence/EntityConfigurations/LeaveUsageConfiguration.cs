using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LeaveUsageConfiguration : IEntityTypeConfiguration<LeaveUsage>
{
    public void Configure(EntityTypeBuilder<LeaveUsage> builder)
    {
        builder.ToTable("LeaveUsages").HasKey(lu => lu.Id);

        builder.Property(lu => lu.Id).HasColumnName("Id").IsRequired();
        builder.Property(lu => lu.EmployeeLeaveId).HasColumnName("EmployeeLeaveId");
        builder.Property(lu => lu.UsageDate).HasColumnName("UsageDate");
        builder.Property(lu => lu.ReturnDate).HasColumnName("ReturnDate");
        builder.Property(lu => lu.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lu => lu.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lu => lu.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasOne(lu => lu.EmployeeLeave)
            .WithMany(el => el.LeaveUsages)
            .HasForeignKey(lu => lu.EmployeeLeaveId);

        builder.HasQueryFilter(lu => !lu.DeletedDate.HasValue);
    }
}