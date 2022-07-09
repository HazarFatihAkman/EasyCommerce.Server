using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Address
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string AddressName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string AddressDetail { get; set; }
    public bool Available { get; set; }
    public DateTime CreatedDate { get; set; }
}
