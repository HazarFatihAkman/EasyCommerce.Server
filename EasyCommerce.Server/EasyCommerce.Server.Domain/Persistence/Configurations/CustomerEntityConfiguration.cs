using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsNotNull();

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .IsRequired();

        builder
            .Property(x => x.IdentityNumber)
            .IsRequired();

        builder
            .Property(x => x.PhoneNumber)
            .IsRequired();

        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder
            .HasMany(x => x.CreditCards)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
    }
}
