using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MachineFileConfiguration : IEntityTypeConfiguration<MachineFile>
{
    public void Configure(EntityTypeBuilder<MachineFile> builder)
    {
        builder.ToTable("MachineFiles");

        builder.Property(mf => mf.Showcase).HasColumnName("Showcase").IsRequired();

        // Many-to-Many relationship with Machines
        builder.HasMany(mf => mf.Machines)
               .WithMany(m => m.MachineFiles)
               .UsingEntity(j => j.ToTable("MachineFileMachines"));
    }
}
