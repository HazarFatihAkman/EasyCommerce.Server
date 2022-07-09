using EasyCommerce.Server.Shared.Domain.Models;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class CreditCardEntity : CreditCard, IMap<CreditCard>
{
    public virtual CustomerEntity Customer { get; set; }
}
