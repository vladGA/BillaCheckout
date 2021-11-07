using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy.PerProduct
{
    public class InMemoryPriceProviderTests
    {
        [Fact(DisplayName = "Check constructor dictionary succeeds")]
        public void CheckConstructorDictionary()
        {
            var priceProvider = new InMemoryPriceProvider(
                new Dictionary<string, decimal>
                {
                    {Apple.AppleCode, 2m },
                    {Banana.BananaCode, 4m }
                });

            Assert.Equal(2, priceProvider.ProductPrices.Count);
        }


        [Fact(DisplayName = "Check if correct quantity product prices")]
        public void CheckProductCount()
        {
            var priceProvider = new InMemoryPriceProvider();
            priceProvider.AddPrice(Apple.AppleCode, 2m);
            priceProvider.AddPrice(Banana.BananaCode, 4m);

            Assert.Equal(2, priceProvider.ProductPrices.Count);
        }

        [Fact(DisplayName = "Check if actual product prices were preserved")]
        public void CheckForProductPrices()
        {
            var priceProvider = new InMemoryPriceProvider();
            priceProvider.AddPrice(Apple.AppleCode, 1m);
            priceProvider.AddPrice(Banana.BananaCode, 4.6m);
            priceProvider.AddPrice(Pineapple.PineappleCode, 12.9m);

            foreach (var pp in priceProvider.ProductPrices)
            {
                if (pp.Key == Apple.AppleCode)
                    Assert.Equal(1m, pp.Value);
                else if (pp.Key == Banana.BananaCode)
                    Assert.Equal(4.6m, pp.Value);
                else Assert.Equal(12.9m, pp.Value);
            }
        }
    }
}
