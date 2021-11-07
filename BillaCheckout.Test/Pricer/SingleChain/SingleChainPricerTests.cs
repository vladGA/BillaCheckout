using BillaCheckout.Core.Pricer.PricerStrategy;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using BillaCheckout.Core.StoreProduct;
using System;
using System.Collections.Generic;
using Xunit;
using BillaCheckout.Core.Pricer.SingleChain;
using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;

namespace BillaCheckout.Test.Pricer.SingleChain
{
    public class SingleChainPricerTests
    {
        private readonly RegularStrategyFactory _strategyFactory;
        public SingleChainPricerTests()
        {
            var unitPriceProvider = new InMemoryPriceProvider();
            unitPriceProvider.AddPrice(Apple.AppleCode, 1m);
            unitPriceProvider.AddPrice(Banana.BananaCode, 0.3m);
            unitPriceProvider.AddPrice(Orange.OrangeCode, 2m);
            unitPriceProvider.AddPrice(Pineapple.PineappleCode, 2m);

            var specialPriceProvider = new InMemorySingleSpecialPriceProvider();
            specialPriceProvider.AddSpecialPrice(Apple.AppleCode, 2, 1.7m);
            specialPriceProvider.AddSpecialPrice(Pineapple.PineappleCode, 3, 5m);

            _strategyFactory = new RegularStrategyFactory(unitPriceProvider, specialPriceProvider);
        }

        [Fact(DisplayName = "Check correct final price")]
        public void CheckPrice()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Orange()),
                new PriceableBasket(new Pineapple()),
                new PriceableBasket(new Pineapple()),
                new PriceableBasket(new Pineapple()),
            };

            var singleChainPricer = new SingleChainPricer(_strategyFactory.GetPricerStrategies());

            Assert.Equal(9.7m, singleChainPricer.Price(pricableBasket));
        }

        [Fact(DisplayName = "Check null constructor throws")]
        public void CheckNullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new SingleChainPricer(null));
        }
    }
}
