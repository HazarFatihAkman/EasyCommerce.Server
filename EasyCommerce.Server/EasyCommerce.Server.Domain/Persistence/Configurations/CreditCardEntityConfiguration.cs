using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class CreditCardEntityConfiguration : IEntityTypeConfiguration<CreditCardEntity>
{
    public void Configure(EntityTypeBuilder<CreditCardEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .HasIndex(x => new { x.CustomerId, x.Number }, "Unique_CreditCard")
            .IsUnique(true);

        builder
            .Property(x => x.CustomerId)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Number)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Cvv)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.ExpMonth)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.ExpYears)
            .ValueGeneratedNever()
            .IsRequired();
    }
}
