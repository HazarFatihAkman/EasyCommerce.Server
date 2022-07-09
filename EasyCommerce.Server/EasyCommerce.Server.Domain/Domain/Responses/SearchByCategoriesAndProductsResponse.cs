using EasyCommerce.Server.Shared.Domain.Models;
using System.Collections.Generic;

namespace EasyCommerce.Server.Shared.Domain.Responses;

public class SearchByCategoriesAndProductsResponse
{
    public List<Category> Categories { get; set; }
    public List<Product> Products { get; set; }
}
