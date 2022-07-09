using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Category
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; }
    public string KeyWords { get; set; }
    public string Description { get; set; }
    public string ImgSrc { get; set; }
    public bool Available { get; set; }
}
