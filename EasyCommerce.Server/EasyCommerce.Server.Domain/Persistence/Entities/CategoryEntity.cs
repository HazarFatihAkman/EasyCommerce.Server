using EasyCommerce.Server.Shared.Domain.Models;
using System;
using System.Collections.Generic;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class CategoryEntity : Category, IMap<Category>
{
    public virtual CategoryEntity Parent { get; set; }
    public virtual ICollection<CategoryEntity> Childeren { get; set; }
    public virtual ICollection<ProductEntity> Products { get; set; }

}
