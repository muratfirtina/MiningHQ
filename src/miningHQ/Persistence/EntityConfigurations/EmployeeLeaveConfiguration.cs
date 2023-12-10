using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeLeaveConfiguration : IEntityTypeConfiguration<EmployeeLeaveUsage>
{
    public void Configure(EntityTypeBuilder<EmployeeLeaveUsage> builder)
    {
        builder.ToTable("EmployeeLeavesUsage").HasKey(el => el.Id);
        
        builder.Property(el => el.Id).HasColumnName("Id").IsRequired();
        builder.Property(el => el.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(el => el.LeaveTypeId).HasColumnName("LeaveTypeId");
        builder.Property(el => el.UsageDate).HasColumnName("UsageDate");
        builder.Property(el => el.ReturnDate).HasColumnName("ReturnDate");
        builder.Property(el => el.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(el => el.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(el => el.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasOne(el => el.Employee)
            .WithMany(e => e.EmployeeLeaveUsages)
            .HasForeignKey(el => el.EmployeeId);
        
        builder
            .HasOne(el => el.LeaveType)
            .WithMany(lt => lt.EmployeeLeaveUsages)
            .HasForeignKey(el => el.LeaveTypeId);
        
        builder.HasQueryFilter(el => !el.DeletedDate.HasValue);
    }
}