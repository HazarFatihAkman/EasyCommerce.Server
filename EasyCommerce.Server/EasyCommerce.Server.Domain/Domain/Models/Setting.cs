using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Setting
{
    public Guid Id { get; set; }
    public string? PrefixKey { get; set; }
    public int Integer { get; set; }
    public string String { get; set; }
    public decimal Decimal { get; set; }
    public double Double { get; set; }
    public DateTime CreatedDate { get; set; }
}
