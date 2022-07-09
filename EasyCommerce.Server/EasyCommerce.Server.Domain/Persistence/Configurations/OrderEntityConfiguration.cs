using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .HasIndex(x => new { x.CustomerId, x.CartId, x.AddressId, x.CargoCompanyId }, "Unique_Order")
            .IsUnique(true);

        builder
            .Property(x => x.CustomerId)
            .ValueGeneratedNever()
            .IsRequired();
        builder
            .Property(x => x.CartId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.CargoCompanyId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .HasMany(x => x.Carts)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        builder
            .HasOne(x => x.Address)
            .WithMany()
            .HasForeignKey(x => x.AddressId);

        builder
            .Property(x => x.CreatedDate)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();
    }
}
