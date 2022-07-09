using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Cart
{
    public Guid Id { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Open { get; set; } = false;
}
