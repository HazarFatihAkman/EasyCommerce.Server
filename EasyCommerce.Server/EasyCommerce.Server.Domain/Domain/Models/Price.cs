using EasyCommerce.Server.Shared.Enums;
using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Price
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? TaxId { get; set; }
    public ProductPriceTypes PriceType { get; set; }
    public SaleChannelTypes SaleChannel { get; set; } 
    public decimal TaxFreePrice { get; set; }
    public double DiscountValue { get; set; }
    public bool IsValidPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}
