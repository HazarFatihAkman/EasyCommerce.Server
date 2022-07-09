using EasyCommerce.Server.Shared.Domain.Models;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class CartEntity : Cart, IMap<Cart>
{
    public virtual CustomerEntity Customer { get; set; }
    public virtual ProductEntity Product { get; set; }
    public virtual OrderEntity Order { get; set; }
}
