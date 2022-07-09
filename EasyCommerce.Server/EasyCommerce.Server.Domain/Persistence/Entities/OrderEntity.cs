using EasyCommerce.Server.Shared.Domain.Models;
using System.Collections.Generic;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class OrderEntity : Order, IMap<Order>
{
    public virtual AddressEntity Address { get; set; }
    public virtual CustomerEntity Customer { get; set; }
    public virtual ICollection<CartEntity> Carts { get; set; }
    public virtual CargoCompanyEntity CargoCompany { get; set; }
}
