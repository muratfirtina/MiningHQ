using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RoleOperationClaimConfiguration : IEntityTypeConfiguration<RoleOperationClaim>
{
    public void Configure(EntityTypeBuilder<RoleOperationClaim> builder)
    {
        builder.ToTable("RoleOperationClaims").HasKey(roc => roc.Id);

        builder.Property(roc => roc.Id).HasColumnName("Id").IsRequired();
        builder.Property(roc => roc.RoleId).HasColumnName("RoleId").IsRequired();
        builder.Property(roc => roc.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();
        builder.Property(roc => roc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(roc => roc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(roc => roc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(roc => !roc.DeletedDate.HasValue);

        builder.HasOne(roc => roc.Role)
            .WithMany(r => r.RoleOperationClaims)
            .HasForeignKey(roc => roc.RoleId);

        builder.HasOne(roc => roc.OperationClaim)
            .WithMany(oc => oc.RoleOperationClaims)
            .HasForeignKey(roc => roc.OperationClaimId);

        builder.HasIndex(roc => new { roc.RoleId, roc.OperationClaimId }).IsUnique();

        builder.HasData(GetSeeds());
    }

    private IEnumerable<RoleOperationClaim> GetSeeds()
    {
        List<RoleOperationClaim> seeds = new();

        // Admin role gets the Admin claim (id: 1)
        seeds.Add(new RoleOperationClaim
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            RoleId = 1, // Admin Role
            OperationClaimId = 1, // Admin Claim
            CreatedDate = DateTime.UtcNow
        });

        // You can add more default role-claim mappings here as needed
        // For example, specific permissions for Moderator and HR Assistant roles

        return seeds;
    }
}
