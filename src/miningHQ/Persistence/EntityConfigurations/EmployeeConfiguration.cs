using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.FirstName).HasColumnName("FirstName");
        builder.Property(e => e.LastName).HasColumnName("LastName");
        builder.Property(e => e.BirthDate).HasColumnName("BirthDate");
        builder.Property(e => e.JobId).HasColumnName("JobId");
        builder.Property(e => e.QuarryId).HasColumnName("QuarryId");
        builder.Property(e => e.Phone).HasColumnName("Phone");
        builder.Property(e => e.Address).HasColumnName("Address");
        builder.Property(e => e.HireDate).HasColumnName("HireDate");
        builder.Property(e => e.DepartureDate).HasColumnName("DepartureDate");
        builder.Property(e => e.LicenseType).HasColumnName("LicenseType");
        builder.Property(e => e.TypeOfBlood).HasColumnName("TypeOfBlood");
        builder.Property(e => e.EmergencyContact).HasColumnName("EmergencyContact");
        builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(e => e.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(e => e.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasOne(e => e.Job).WithMany(j => j.Employees).HasForeignKey(e => e.JobId);
        builder.HasOne(e => e.Quarry).WithMany(q => q.Employees).HasForeignKey(e => e.QuarryId);
        builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);
        builder.HasMany(e => e.Machines).WithMany(m => m.Employees);
        builder.HasMany(e => e.EmployeeLeaveUsages).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);
        builder.HasMany(e => e.EntitledLeaves).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);
        builder.HasMany(e => e.Timekeepings).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);
        builder.HasMany(e => e.Overtimes).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);

        builder.HasQueryFilter(e => !e.DeletedDate.HasValue);
    }
}