using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial
{
    public interface ISingleSpecialPriceProvider
    {
        Dictionary<(string Product, int Quantity), decimal> Specials { get; }
    }
}
