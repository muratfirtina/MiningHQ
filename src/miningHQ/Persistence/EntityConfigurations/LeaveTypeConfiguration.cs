using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveTypes").HasKey(lt => lt.Id);
        
        builder.Property(lt => lt.Id).HasColumnName("Id").IsRequired();
        builder.Property(lt => lt.Name).HasColumnName("Name");
        builder.Property(lt => lt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lt => lt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lt => lt.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasMany(lt => lt.EmployeeLeaveUsages).WithOne(el => el.LeaveType).HasForeignKey(el => el.LeaveTypeId);
       
        builder.HasQueryFilter(lt => !lt.DeletedDate.HasValue);
    }
}