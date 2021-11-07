using System.Collections.Generic;
using System.Linq;
using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.Pricer.PricerStrategy.Aggregation;
using BillaCheckout.Core.StoreProduct;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy.Aggregation
{
    public class AllSumStrategyTests
    {
        [Fact(DisplayName = "Check if all sum is correct")]
        public void CheckAllSum()
        {
            ICollection<PriceableBasket> priceableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(1m, new Apple()),
                new PriceableBasket(1m, new Apple()),
                new PriceableBasket(1m, new Apple()),
                new PriceableBasket(2m, new Pineapple()),
                new PriceableBasket(1.8m, new List<Product> {new Apple(),new Apple()}),
                new PriceableBasket(1.8m, new List<Product> {
                    new Pineapple(),new Pineapple(), new Pineapple()}),
            };

            var pricedBasket = new AllSumStrategy().Price(priceableBasket);

            Assert.Equal(1, pricedBasket.Count);
            Assert.Equal(8.6m, pricedBasket.First().Price);
        }
    }
}
