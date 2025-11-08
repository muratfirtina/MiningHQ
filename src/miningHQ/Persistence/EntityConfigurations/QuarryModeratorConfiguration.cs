using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuarryModeratorConfiguration : IEntityTypeConfiguration<QuarryModerator>
{
    public void Configure(EntityTypeBuilder<QuarryModerator> builder)
    {
        
        builder.ToTable("QuarryModerators");
        builder.HasKey(qm => qm.Id);
        
        builder.Property(qm => qm.UserId).IsRequired();
        builder.Property(qm => qm.QuarryId).IsRequired();
        builder.Property(qm => qm.CreatedDate).IsRequired();
        builder.Property(qm => qm.CreatedBy).IsRequired();
        
        // User relationship
        builder.HasOne(qm => qm.User)
            .WithMany()
            .HasForeignKey(qm => qm.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Quarry relationship
        builder.HasOne(qm => qm.Quarry)
            .WithMany()
            .HasForeignKey(qm => qm.QuarryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Composite unique index
        builder.HasIndex(qm => new { qm.UserId, qm.QuarryId })
            .IsUnique()
            .HasDatabaseName("IX_QuarryModerators_UserId_QuarryId");
    }
    
}