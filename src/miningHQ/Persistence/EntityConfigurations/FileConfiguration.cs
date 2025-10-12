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
        builder.Property(f => f.Category).HasColumnName("Category");
        builder.Property(f => f.Storage).HasColumnName("Storage");
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");
        
        // TPT (Table Per Type) strategy - Her subclass kendi tablosuna sahip
        // Discriminator kolonu KULLANILMAZ
        builder.UseTptMappingStrategy();

        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
    }
}
