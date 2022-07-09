using EasyCommerce.Server.Shared.Domain.Models;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class AddressEntity : Address, IMap<Address>
{
    public virtual CustomerEntity Customer { get; set; }
}
