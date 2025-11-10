using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles").HasKey(ur => ur.Id);

        builder.Property(ur => ur.Id).HasColumnName("Id").IsRequired();
        builder.Property(ur => ur.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ur => ur.RoleId).HasColumnName("RoleId").IsRequired();
        builder.Property(ur => ur.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ur => ur.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ur => ur.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ur => !ur.DeletedDate.HasValue);

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

        builder.HasData(GetSeeds());
    }

    private IEnumerable<UserRole> GetSeeds()
    {
        List<UserRole> seeds = new();

        // Assign Admin role to the default admin user
        seeds.Add(new UserRole
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
            UserId = Guid.Parse("729c40f5-0859-48d7-a388-451520c1289c"), // Default admin user from UserOperationClaimConfiguration
            RoleId = 1, // Admin Role
            CreatedDate = DateTime.UtcNow
        });

        return seeds;
    }
}
