using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TimekeepingConfiguration : IEntityTypeConfiguration<Timekeeping>
{
    public void Configure(EntityTypeBuilder<Timekeeping> builder)
    {
        builder.ToTable("Timekeepings").HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Date).HasColumnName("Date");
        builder.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(t => t.Status).HasColumnName("Status");
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasOne(t => t.Employee).WithMany(e => e.Timekeepings).HasForeignKey(t => t.EmployeeId);

        builder.HasQueryFilter(t => !t.DeletedDate.HasValue);
        
        
    }
}