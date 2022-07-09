using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Shared;

public interface IApplicationDbContext
{
    DbSet<UserEntity> Users { get; set; }
    DbSet<CustomerEntity> Customers { get; set; }
    DbSet<ProductEntity> Products { get; set; }
    DbSet<PriceEntity> Prices { get; set; }
    DbSet<CategoryEntity> Categories { get; set; }
    DbSet<OrderEntity> Orders { get; set; }
    DbSet<CartEntity> Carts { get; set; }
    DbSet<AddressEntity> Address { get; set; }
    DbSet<TaxEntity> Taxes { get; set; }
    DbSet<CargoCompanyEntity> CargoCompanies { get; set; }
    DbSet<SettingEntity> Settings { get; set; }
    DbSet<CreditCardEntity> CreditCards { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
