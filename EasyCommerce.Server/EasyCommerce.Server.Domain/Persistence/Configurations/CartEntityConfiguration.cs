using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
{
    public void Configure(EntityTypeBuilder<CartEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.CustomerId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.ProductId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder
            .Property(x => x.CreatedDate)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();
    }
}
