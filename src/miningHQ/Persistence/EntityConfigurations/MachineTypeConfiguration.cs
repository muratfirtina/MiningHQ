using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MachineTypeConfiguration : IEntityTypeConfiguration<MachineType>
{
    public void Configure(EntityTypeBuilder<MachineType> builder)
    {
        builder.ToTable("MachineTypes").HasKey(mt => mt.Id);

        builder.Property(mt => mt.Id).HasColumnName("Id").IsRequired();
        builder.Property(mt => mt.Name).HasColumnName("Name");
        builder.Property(mt => mt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(mt => mt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(mt => mt.DeletedDate).HasColumnName("DeletedDate");
        
        

        builder.HasQueryFilter(mt => !mt.DeletedDate.HasValue);
    }
}