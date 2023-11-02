using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs").HasKey(j => j.Id);

        builder.Property(j => j.Id).HasColumnName("Id").IsRequired();
        builder.Property(j => j.Name).HasColumnName("Name");
        builder.Property(j => j.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(j => j.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(j => j.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasQueryFilter(j => !j.DeletedDate.HasValue);
    }
}