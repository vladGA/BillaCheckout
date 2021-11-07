using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy.PerProduct
{
    public interface IProductUnitPriceProvider
    {
        Dictionary<string, decimal> ProductPrices { get; }
    }
}
