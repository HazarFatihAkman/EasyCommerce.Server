using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Tax
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Percent { get; set; }
}
