using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCommerce.Server.Shared.Persistence.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .IsClustered(false);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .HasIndex(x => x.Title)
            .IsUnique();

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .Property(x => x.ImgSrc)
            .IsRequired();

        builder
            .HasMany(x => x.Childeren)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .HasPrincipalKey(x => x.Id);

        builder
            .HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .HasPrincipalKey(x => x.Id);

    }
}
