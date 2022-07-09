using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string KeyWords { get; set; }
    public string Barcode { get; set; }
    public int Stock { get; set; }
    public string ImgSrc { get; set; }
    public bool Available { get; set; }
    public DateTime CreatedDate { get; set; }
}
