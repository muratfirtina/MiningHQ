using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuarryFileConfiguration : IEntityTypeConfiguration<QuarryFile>
{
    public void Configure(EntityTypeBuilder<QuarryFile> builder)
    {
        builder.ToTable("QuarryFiles");

        builder.Property(qf => qf.Showcase).HasColumnName("Showcase").IsRequired();

        // Many-to-Many relationship with Quarries
        builder.HasMany(qf => qf.Quarries)
               .WithMany(q => q.QuarryFiles)
               .UsingEntity(j => j.ToTable("QuarryFileQuarries"));
    }
}
