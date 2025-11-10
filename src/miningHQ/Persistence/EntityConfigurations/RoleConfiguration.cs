using Core.Security.Entities;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        builder.Property(r => r.Description).HasColumnName("Description").HasMaxLength(500);
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);

        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        builder.HasMany(r => r.RoleOperationClaims)
            .WithOne(roc => roc.Role)
            .HasForeignKey(roc => roc.RoleId);

        builder.HasIndex(r => r.Name).IsUnique();

        builder.HasData(GetSeeds());
    }

    private IEnumerable<Role> GetSeeds()
    {
        List<Role> seeds = new()
        {
            new Role { Id = 1, Name = Roles.Admin, Description = "Full system access with all permissions", CreatedDate = DateTime.UtcNow },
            new Role { Id = 2, Name = Roles.Moderator, Description = "Quarry moderator with limited access", CreatedDate = DateTime.UtcNow },
            new Role { Id = 3, Name = Roles.HRAssistant, Description = "HR assistant with employee management access", CreatedDate = DateTime.UtcNow }
        };

        return seeds;
    }
}
