using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
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
            .HasIndex(x => new {x.AddressName, x.CustomerId}, "Unique_Address")
            .IsUnique(true);

        builder
            .Property(x => x.AddressName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.City)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Country)
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(x => x.Street)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.PostalCode)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.AddressDetail)
            .HasMaxLength(250);

        builder
            .Property(x => x.CreatedDate)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();

    }
}
