using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial
{
    public class InMemorySingleSpecialPriceProvider : ISingleSpecialPriceProvider
    {
        private Dictionary<(string Product, int Quantity), decimal> _prices;

        public InMemorySingleSpecialPriceProvider()
        {
            _prices = new Dictionary<(string Product, int Quantity), decimal>();
        }

        public InMemorySingleSpecialPriceProvider(Dictionary<(string Product, int Quantity), decimal> prices)
        {
            _prices = new Dictionary<(string Product, int Quantity), decimal>(prices);
        }

        public void AddSpecialPrice(string productCode, int quantity, decimal price)
        {
            _prices.Add((productCode, quantity), price);
        }
        public Dictionary<(string Product, int Quantity), decimal> Specials => _prices;
    }
}
