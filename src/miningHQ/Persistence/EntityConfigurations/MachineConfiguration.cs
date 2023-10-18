using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.ToTable("Machines").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.ModelId).HasColumnName("ModelId");
        builder.Property(m => m.QuarryId).HasColumnName("QuarryId");
        builder.Property(m => m.SerialNumber).HasColumnName("SerialNumber");
        builder.Property(m => m.Name).HasColumnName("Name");
        builder.Property(m => m.MachineTypeId).HasColumnName("MachineTypeId");
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasOne(m => m.Model)
            .WithMany(m => m.Machines)
            .HasForeignKey(m => m.ModelId);
        
        builder
            .HasOne(m => m.Quarry)
            .WithMany(q => q.Machines)
            .HasForeignKey(m => m.QuarryId);
        
        builder
            .HasOne(m => m.MachineType)
            .WithMany(mt => mt.Machines)
            .HasForeignKey(m => m.MachineTypeId);

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
    }
}