using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EntitledLeaveConfiguration : IEntityTypeConfiguration<EntitledLeave>
{
    public void Configure(EntityTypeBuilder<EntitledLeave> builder)
    {
        builder.ToTable("EntitledLeaves").HasKey(el => el.Id);

        builder.Property(el => el.Id).HasColumnName("Id").IsRequired();
        builder.Property(el => el.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(el => el.LeaveTypeId).HasColumnName("LeaveTypeId");
        builder.Property(el => el.EntitledDate).HasColumnName("EntitledDate");
        builder.Property(el => el.EntitledDays).HasColumnName("EntitledDays");
        builder.Property(el => el.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(el => el.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(el => el.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(el => !el.DeletedDate.HasValue);
    }
}