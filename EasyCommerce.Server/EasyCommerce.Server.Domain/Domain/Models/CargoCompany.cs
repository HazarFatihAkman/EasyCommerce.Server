using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class CargoCompany
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string WebSite { get; set; }
    public decimal CargoPrice { get; set; }
    public bool Available { get; set; }
}
