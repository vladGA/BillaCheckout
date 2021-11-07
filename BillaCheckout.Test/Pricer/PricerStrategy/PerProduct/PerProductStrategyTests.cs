using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using BillaCheckout.Core.StoreProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy.PerProduct
{
    public class PerProductStrategyTests
    {
        private InMemoryPriceProvider _perProductPrices;

        private void ConfigurePerProductPrices()
        {
            _perProductPrices = new InMemoryPriceProvider();
            _perProductPrices.AddPrice(Apple.AppleCode, 0.5m);
            _perProductPrices.AddPrice(Banana.BananaCode, 0.7m);
            _perProductPrices.AddPrice(Orange.OrangeCode, 0.45m);
        }

        public PerProductStrategyTests()
        {
            ConfigurePerProductPrices();
        }

        [Fact(DisplayName = "Check if unknown product throws")]
        public void CheckUnknownProduct()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new List<Product> {new Apple(), new Orange() }),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Pineapple()),
            };

            Assert.Throws<Exception>(() =>
            {
                new PerProductStrategy(_perProductPrices).Price(pricableBasket);
            });
        }

        [Fact(DisplayName = "Check if pricer does not loose composite basket")]
        public void CheckCompositeBasketIntegrity()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new List<Product> {new Apple(), new Orange() }),
                new PriceableBasket(new Apple()),
            };

            var pricedBasket = new PerProductStrategy(_perProductPrices)
                                    .Price(pricableBasket);

            Assert.Equal(1, pricedBasket.Count(s => !s.HasSingleProduct));
            Assert.Equal(1, pricedBasket.Count(s => s.HasSingleProduct && s.FirstProduct is Apple));
        }

        [Fact(DisplayName = "Check if pricer does not loose single baskets")]
        public void CheckBasketIntegrity()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Banana()),
                new PriceableBasket(new Banana()),
                new PriceableBasket(new Orange()),
                new PriceableBasket(new Orange()),
                new PriceableBasket(new Orange()),
                new PriceableBasket(new Orange())
            };

            var pricedBasket = new PerProductStrategy(_perProductPrices).Price(pricableBasket);

            Assert.Equal(9, pricedBasket.Count);
            Assert.Equal(3, pricedBasket.Count(prod => prod.FirstProduct is Apple));
            Assert.Equal(2, pricedBasket.Count(prod => prod.FirstProduct is Banana));
            Assert.Equal(4, pricedBasket.Count(prod => prod.FirstProduct is Orange));
        }

        [Fact(DisplayName = "Check if all single pricable baskets are priced")]
        public void CheckSinglePricableBasket()
        {
            ICollection<PriceableBasket> pricableBasket = new List<PriceableBasket>
            {
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Apple()),
                new PriceableBasket(new Banana()),
                new PriceableBasket(new Banana()),
                new PriceableBasket(new Orange())
            };

            var pricedBasket = new PerProductStrategy(_perProductPrices).Price(pricableBasket);

            foreach (var basket in pricedBasket)
            {
                var product = basket.FirstProduct;
                if (product is Apple)
                    Assert.Equal(0.5m, basket.Price);
                else if (product is Banana)
                    Assert.Equal(0.7m, basket.Price);
                else if (product is Orange)
                    Assert.Equal(0.45m, basket.Price);
            }
        }
    }
}
