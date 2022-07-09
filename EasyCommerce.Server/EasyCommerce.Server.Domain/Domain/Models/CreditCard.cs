using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class CreditCard
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string Name { get; set; }
    public string? Number { get; set; }
    public int Cvv { get; set; }
    public int ExpMonth { get; set; }
    public int ExpYears { get; set; }
}
