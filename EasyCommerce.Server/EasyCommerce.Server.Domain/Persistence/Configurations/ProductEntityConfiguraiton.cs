using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class ProductEntityConfiguraiton : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsNotNull();

        builder
            .Property(x => x.CategoryId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .IsRequired();

        builder
            .Property(x => x.KeyWords)
            .IsRequired();

        builder
            .Property(x => x.Barcode)
            .IsRequired();

        builder
            .Property(x => x.Barcode)
            .IsUnicode();

        builder
            .Property(x => x.Stock)
            .IsRequired();

        builder
            .Property(x => x.ImgSrc)
            .IsRequired();

        builder
            .HasMany(x => x.Prices)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);

        builder 
            .Property(x => x.CreatedDate)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();
    }
}
