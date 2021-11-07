using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;
using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy.SingleSpecial
{
    public class SingleSpecialStrategyTests
    {
        InMemorySingleSpecialPriceProvider _priceProvider;
        public SingleSpecialStrategyTests()
        {
            _priceProvider = new InMemorySingleSpecialPriceProvider();
            _priceProvider.AddSpecialPrice(Apple.AppleCode, 2, 1.5m);
            _priceProvider.AddSpecialPrice(Pineapple.PineappleCode, 3, 2.75m);
        }

        [Fact(DisplayName = "Check if product aggregation works")]
        public void CheckIfAggregationWorks()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Pineapple()),
                new PriceableBasket(new Pineapple()),
                new PriceableBasket(new Pineapple()),
            };

            var pricedBasket = new SingleSpecialStrategy(_priceProvider)
                                    .Price(pricableBasket);

            Assert.Equal(3, pricedBasket.Count);
            Assert.Equal(1.5m, pricedBasket
                .First(p => p.Items.Count == 2 && p.Items.All(s => s is Apple)).Price);
            Assert.Equal(2.75m, pricedBasket
                .First(p => p.Items.Count == 3 && p.Items.All(s => s is Pineapple)).Price);
        }
    }
}
