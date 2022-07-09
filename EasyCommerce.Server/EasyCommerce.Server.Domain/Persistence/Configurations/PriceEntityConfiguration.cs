using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class PriceEntityConfiguration : IEntityTypeConfiguration<PriceEntity>
{
    public void Configure(EntityTypeBuilder<PriceEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsNotNull();

        builder
            .Property(x => x.ProductId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .HasIndex(x => new { x.ProductId, x.TaxId, x.TaxFreePrice, x.PriceType }, "Unique_Price")
            .IsUnique(true);

        builder
            .Property(x => x.TaxId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.PriceType)
            .IsRequired();
        
        builder
            .Property(x => x.SaleChannel)
            .IsRequired();

        builder
            .Property(x => x.TaxFreePrice)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();
    }
}
