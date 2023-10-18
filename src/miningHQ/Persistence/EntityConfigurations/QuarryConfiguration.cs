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
        builder.Property(q => q.Name).HasColumnName("Name");
        builder.Property(q => q.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(q => q.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(q => q.DeletedDate).HasColumnName("DeletedDate");
        

        builder.HasQueryFilter(q => !q.DeletedDate.HasValue);
    }
}