using BillaCheckout.Core.Pricer.PricerStrategy;
using BillaCheckout.Core.Util;
using System.Collections.Generic;
using System.Linq;

namespace BillaCheckout.Core.Pricer.SingleChain
{
    public class SingleChainPricer : BasePricer
    {
        private readonly IList<IPricerStrategy> _strategies;

        public SingleChainPricer(IList<IPricerStrategy> strategies)
        {
            ArgumentValidation.EnsureNotNull(strategies, nameof(strategies));

            _strategies = strategies;
        }

        public override decimal Price(ICollection<PriceableBasket> basketPrices)
        {
            var nextPrices = basketPrices;

            foreach (var strategy in _strategies)
            {
                nextPrices = strategy.Price(nextPrices);
            }

            return nextPrices.First().Price;
        }
    }
}
