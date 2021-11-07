using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy
{
    public interface IPricerStrategy
    {
        ICollection<PriceableBasket> Price(ICollection<PriceableBasket> basketPrices);
    }
}
