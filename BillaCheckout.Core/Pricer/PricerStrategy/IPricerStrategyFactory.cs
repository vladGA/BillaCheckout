using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy
{
    public interface IPricerStrategyFactory
    {
        IList<IPricerStrategy> GetPricerStrategies();
    }
}
