using EasyCommerce.Server.Shared.Enums;
using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? CartId { get; set; }
    public Guid? AddressId { get; set; }
    public Guid? CargoCompanyId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime GaveToCargoDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public PaymentTypes PaymentType { get; set; }
    public string CargoFollowNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
