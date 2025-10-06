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
        builder.Property(m => m.CurrentOperatorId).HasColumnName("CurrentOperatorId");
        builder.Property(m => m.PurchaseDate).HasColumnName("PurchaseDate");
        builder.Property(m => m.Description).HasColumnName("Description");
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
        
        // CurrentOperator relationship - many machines can have the same current operator
        // but we don't need inverse navigation property on Employee
        builder
            .HasOne(m => m.CurrentOperator)
            .WithMany() // No inverse navigation property
            .HasForeignKey(m => m.CurrentOperatorId)
            .OnDelete(DeleteBehavior.SetNull); // If employee is deleted, set CurrentOperatorId to null

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
    }
}