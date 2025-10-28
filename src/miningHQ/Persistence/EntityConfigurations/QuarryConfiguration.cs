using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuarryConfiguration : IEntityTypeConfiguration<Quarry>
{
    public void Configure(EntityTypeBuilder<Quarry> builder)
    {
        builder.ToTable("Quarries").HasKey(q => q.Id);

        builder.Property(q => q.Id).HasColumnName("Id").IsRequired();
        builder.Property(q => q.Name).HasColumnName("Name").IsRequired();
        builder.Property(q => q.Description).HasColumnName("Description");
        builder.Property(q => q.Location).HasColumnName("Location");
        builder.Property(q => q.Latitude).HasColumnName("Latitude");
        builder.Property(q => q.Longitude).HasColumnName("Longitude");
        builder.Property(q => q.CoordinateDescription).HasColumnName("CoordinateDescription");
        builder.Property(q => q.MiningEngineerId).HasColumnName("MiningEngineerId");
        builder.Property(q => q.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(q => q.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(q => q.DeletedDate).HasColumnName("DeletedDate");
        
        // İlişkiler
        builder.HasOne(q => q.MiningEngineer)
            .WithMany()
            .HasForeignKey(q => q.MiningEngineerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(q => q.Employees)
            .WithOne(e => e.Quarry)
            .HasForeignKey(e => e.QuarryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(q => q.Machines)
            .WithOne(m => m.Quarry)
            .HasForeignKey(m => m.QuarryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(q => q.QuarryProductions)
            .WithOne(qp => qp.Quarry)
            .HasForeignKey(qp => qp.QuarryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(q => !q.DeletedDate.HasValue);
    }
}