using System.Collections.Generic;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;
using BillaCheckout.Core.Pricer.PricerStrategy.Aggregation;

namespace BillaCheckout.Core.Pricer.PricerStrategy
{
    public class RegularStrategyFactory : IPricerStrategyFactory
    {
        private readonly IProductUnitPriceProvider _unitPriceProvider;
        private readonly ISingleSpecialPriceProvider _singleSpecialPriceProvider;

        public RegularStrategyFactory(IProductUnitPriceProvider unitPriceProvider,
            ISingleSpecialPriceProvider singleSpecialPriceProvider)
        {
            _unitPriceProvider = unitPriceProvider;
            _singleSpecialPriceProvider = singleSpecialPriceProvider;
        }

        public IList<IPricerStrategy> GetPricerStrategies()
        {
            return new List<IPricerStrategy>
            {
                new PerProductStrategy(_unitPriceProvider),
                new SingleSpecialStrategy(_singleSpecialPriceProvider),
                new AllSumStrategy()
            };
        }
    }
}
