using EasyCommerce.Server.Shared.Domain.Models;
using System.Collections.Generic;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class CustomerEntity : Customer, IMap<Customer>
{
    public virtual ICollection<OrderEntity> Orders { get; set; }
    public virtual ICollection<AddressEntity> Addresses { get; set; }
    public virtual ICollection<CreditCardEntity> CreditCards { get; set; }
}
