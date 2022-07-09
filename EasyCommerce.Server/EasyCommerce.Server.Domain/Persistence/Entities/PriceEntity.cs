using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Enums;

namespace EasyCommerce.Server.Shared.Persistence.Entities;

public class PriceEntity : Price, IMap<Price>
{
    public virtual ProductEntity Product { get; set; }
    public virtual TaxEntity Tax { get; set; }
    public decimal Price { get => PriceCalcute(); }
    private decimal PriceCalcute()
    {
        switch (PriceType)
        {
            case ProductPriceTypes.TaxPrice:
            case ProductPriceTypes.OldTaxPrice:
                return TaxFreePrice * (((decimal)Tax.Percent / 100) + 1);

            case ProductPriceTypes.TaxFreePrice:
            case ProductPriceTypes.OldTaxFreePrice:
                return TaxFreePrice;

            case ProductPriceTypes.DiscountPrice:
                return (((100 - (decimal)DiscountValue) / 100) * TaxFreePrice) * (((decimal)Tax.Percent / 100) + 1);
            default:
                return TaxFreePrice;
        }
    }
}
