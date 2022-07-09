using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class TaxEntityConfiguration : IEntityTypeConfiguration<TaxEntity>
{
    public void Configure(EntityTypeBuilder<TaxEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.Percent)
            .IsRequired();
    }
}
