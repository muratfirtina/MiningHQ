using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Domain.Entities.File;

namespace Persistence.EntityConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("Files").HasKey(f => f.Id);

        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
        builder.Property(f => f.Name).HasColumnName("Name");
        builder.Property(f => f.Path).HasColumnName("Path");
        builder.Property(f => f.Storage).HasColumnName("Storage");
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");
        
        

        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
    }
}