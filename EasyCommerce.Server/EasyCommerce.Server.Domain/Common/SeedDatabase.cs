using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using EasyCommerce.Server.Shared.Enums;

namespace EasyCommerce;

public class SeedDatabase
{
    private readonly ModelBuilder _modelBuilder;
    protected Guid UserId = Guid.Parse("fed8206e-3a16-416f-99d4-141e760ea20b");
    protected Guid CustomerId = Guid.Parse("8a270105-a039-45b0-90e9-62e6886894e2");
    protected Guid CategoryId0 = Guid.Parse("723de3b8-6c07-4e8c-b1fc-184b482d9c4d");
    protected Guid CategoryId1 = Guid.Parse("723de3b8-6c07-4e8c-b1fc-1844482d9c4d");
    protected Guid SubCategoryId = Guid.Parse("7cf92a91-ba8d-495d-a1fa-2a6fad2012a8");
    protected Guid TaxId;
    protected Guid ProductId = Guid.Parse("0ccc0e7e-bdec-4e75-b9b3-aa831952311d");
    protected Guid PriceId = Guid.Parse("aba0000f-eb0b-4aeb-923e-0f5f9ccfa989");
    public SeedDatabase(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
        TaxId = Guid.NewGuid();
    }
    public void SeedAllDatabase()
    {
        SeedDatabaseUser();
        SeedDatabaseCustomer();

        SeedDatabaseTax();

        SeedDatabaseCategory();
        SeedDatabaseProduct();
        SeedDatabasePrice();
        SeedDatabaseCreditCard();
    }
    public void SeedDatabaseUser()
    {
        _modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Easy Commerce",
                    Email = "admin@gmail.com",
                    Password = "Admin123!",
                    Pin = "1",
                    ApplicationRolesUser = ApplicationRolesUser.Admin
                }
            );
    }
    public void SeedDatabaseCustomer()
    {
        _modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity
                {
                    Id = CustomerId,
                    FirstName = "Hazar Fatih",
                    LastName = "Akman",
                    Email = "akman.hazar.fatih@gmail.com",
                    Password = "123",
                    IdentityNumber = "30025022756",
                    PhoneNumber = "5536803185",
                    Available = true
                }
            );
    }
    public void SeedDatabaseTax()
    {
        _modelBuilder.Entity<TaxEntity>().HasData(
                new TaxEntity
                {
                    Id = TaxId,
                    Name = "standard tax",
                    Percent = 18
                }
            );
    }
    public void SeedDatabaseCategory()
    {
        var sub1Id = Guid.NewGuid();
        var sub2Id = Guid.NewGuid();
        _modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity
                {
                    Id = CategoryId0,
                    Title = "Main -> 0 Category",
                    ImgSrc = "test 0",
                    Description = "test 0",
                    KeyWords = "test 0",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = SubCategoryId,
                    Title = "Sub Category",
                    ParentId = CategoryId0,
                    ImgSrc = "test 1",
                    Description = "test 1",
                    KeyWords = "test 1",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = sub1Id,
                    Title = "Sub -> 0 Category",
                    ParentId = SubCategoryId,
                    ImgSrc = "test 2",
                    Description = "test 2",
                    KeyWords = "test 2",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Sub -> 1 Category",
                    ParentId = sub1Id,
                    ImgSrc = "test 3",
                    Description = "test 32",
                    KeyWords = "test 3",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = CategoryId1,
                    Title = "Main -> 1 Category",
                    ImgSrc = "test 0",
                    Description = "test 0",
                    KeyWords = "test 0",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = sub2Id,
                    Title = "Sub Category / Main 1",
                    ParentId = CategoryId1,
                    ImgSrc = "test 1",
                    Description = "test 1",
                    KeyWords = "test 1",
                    Available = true
                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Sub -> 0 Category / Main 1",
                    ParentId = sub2Id,
                    ImgSrc = "test 2",
                    Description = "test 2",
                    KeyWords = "test 2",
                    Available = true
                }
            );
    }

    public void SeedDatabaseProduct()
    {
        _modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity
            {
                Id = ProductId,
                CategoryId = SubCategoryId,
                Title = "Product 1",
                Barcode = "1",
                KeyWords = "test",
                Description = "test",
                Stock = 10,
                ImgSrc = "test",
            });
    }

    public void SeedDatabasePrice()
    {
        _modelBuilder.Entity<PriceEntity>().HasData(
            new PriceEntity
            {
                Id = PriceId,
                IsValidPrice = true,
                TaxFreePrice = decimal.Parse("20.5"),
                PriceType = ProductPriceTypes.DiscountPrice,
                ProductId = ProductId,
                TaxId = TaxId,
                SaleChannel = SaleChannelTypes.WebSite,
                DiscountValue = 8
            });
        _modelBuilder.Entity<PriceEntity>().HasData(
            new PriceEntity
            {
                Id = Guid.NewGuid(),
                IsValidPrice = false,
                TaxFreePrice = decimal.Parse("20.5"),
                PriceType = ProductPriceTypes.OldTaxPrice,
                ProductId = ProductId,
                TaxId = TaxId,
                SaleChannel = SaleChannelTypes.WebSite
            });
    }
    public void SeedDatabaseCreditCard()
    {
        _modelBuilder.Entity<CreditCardEntity>().HasData(
            new CreditCardEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = CustomerId,
                Name = "Save card 1",
                Number = "5400360000000003",
                Cvv = 151,
                ExpMonth = 12,
                ExpYears = 25
            });
    }
}
