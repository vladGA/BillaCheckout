using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy.PerProduct
{
    public class InMemoryPriceProvider : IProductUnitPriceProvider
    {
        private readonly Dictionary<string, decimal> _prices;

        public InMemoryPriceProvider()
        {
            _prices = new Dictionary<string, decimal>();
        }

        public InMemoryPriceProvider(Dictionary<string, decimal> prices)
        {
            _prices = new Dictionary<string, decimal>(prices);
        }

        public void AddPrice(string productCode, decimal price)
        {
            _prices.Add(productCode, price);
        }

        public Dictionary<string, decimal> ProductPrices => _prices;
    }
}
