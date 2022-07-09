using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class ProductEntity : Product, IMap<Product>
{
    public virtual CategoryEntity Category { get; set; }
    public virtual ICollection<PriceEntity> Prices { get; set; }
    public virtual PriceEntity CurrentPrice { get => Prices.FirstOrDefault(x => x.IsValidPrice == true); }
    public virtual PriceEntity OldPrice { get => Prices.OrderBy(x => x.CreatedAt.Ticks).FirstOrDefault(x => x.IsValidPrice == false && x.PriceType == ProductPriceTypes.OldTaxPrice); }

}
