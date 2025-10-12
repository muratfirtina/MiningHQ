using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeFileConfiguration : IEntityTypeConfiguration<EmployeeFile>
{
    public void Configure(EntityTypeBuilder<EmployeeFile> builder)
    {
        builder.ToTable("EmployeeFiles");

        builder.Property(ef => ef.Showcase).HasColumnName("Showcase").IsRequired();

        // Many-to-Many relationship with Employees
        builder.HasMany(ef => ef.Employees)
               .WithMany(e => e.EmployeeFiles)
               .UsingEntity(j => j.ToTable("EmployeeFileEmployees"));
    }
}
