using EasyCommerce.Server.Shared.Persistence.Configurations;
using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Shared;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<PriceEntity> Prices { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CartEntity> Carts { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<AddressEntity> Address { get; set; }
    public DbSet<TaxEntity> Taxes { get; set; }
    public DbSet<CargoCompanyEntity> CargoCompanies { get; set; }
    public DbSet<SettingEntity> Settings { get; set; }
    public DbSet<CreditCardEntity> CreditCards { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new UserEntityConfiguration().Configure(modelBuilder.Entity<UserEntity>());
        new CustomerEntityConfiguration().Configure(modelBuilder.Entity<CustomerEntity>());
        new ProductEntityConfiguraiton().Configure(modelBuilder.Entity<ProductEntity>());
        new CategoryEntityConfiguration().Configure(modelBuilder.Entity<CategoryEntity>());
        new PriceEntityConfiguration().Configure(modelBuilder.Entity<PriceEntity>());
        new CartEntityConfiguration().Configure(modelBuilder.Entity<CartEntity>());
        new OrderEntityConfiguration().Configure(modelBuilder.Entity<OrderEntity>());
        new AddressEntityConfiguration().Configure(modelBuilder.Entity<AddressEntity>());
        new TaxEntityConfiguration().Configure(modelBuilder.Entity<TaxEntity>());
        new CargoCompanyEntityConfiguration().Configure(modelBuilder.Entity<CargoCompanyEntity>());
        new SettingEntityConfiguration().Configure(modelBuilder.Entity<SettingEntity>());
        new CreditCardEntityConfiguration().Configure(modelBuilder.Entity<CreditCardEntity>());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductEntityConfiguraiton).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PriceEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaxEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CargoCompanyEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SettingEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreditCardEntity).Assembly);

        var seedDatabase = new SeedDatabase(modelBuilder);
        seedDatabase.SeedAllDatabase();
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }
}
