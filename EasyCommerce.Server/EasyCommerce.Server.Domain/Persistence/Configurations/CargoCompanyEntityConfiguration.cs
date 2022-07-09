using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class CargoCompanyEntityConfiguration : IEntityTypeConfiguration<CargoCompanyEntity>
{
    public void Configure(EntityTypeBuilder<CargoCompanyEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.CompanyName)
            .IsNotNull();

        builder
            .Property(x => x.WebSite)
            .IsNotNull();
    }
}
