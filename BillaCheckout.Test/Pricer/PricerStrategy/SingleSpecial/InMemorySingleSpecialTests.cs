using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy.SingleSpecial
{
    public class InMemorySingleSpecialTests
    {
        [Fact(DisplayName = "Check if dictionary constructor works")]
        public void CheckDictionaryConstructor()
        {
            var priceProvider = new InMemorySingleSpecialPriceProvider(
                new Dictionary<(string Product, int Quantity), decimal>
                {
                    {(Apple.AppleCode, 2), 2m },
                    {(Banana.BananaCode, 3), 4m },
                });

            Assert.Equal(2, priceProvider.Specials.Count);
        }

        [Fact(DisplayName = "Check if correct quantity product prices")]
        public void CheckProductCount()
        {
            var priceProvider = new InMemorySingleSpecialPriceProvider();
            priceProvider.AddSpecialPrice(Apple.AppleCode, 2, 2m);
            priceProvider.AddSpecialPrice(Banana.BananaCode, 3, 4m);

            Assert.Equal(2, priceProvider.Specials.Count);
        }

        [Fact(DisplayName = "Check if actual product prices were preserved")]
        public void CheckForProductPrices()
        {
            var priceProvider = new InMemorySingleSpecialPriceProvider();
            priceProvider.AddSpecialPrice(Apple.AppleCode, 2, 1m);
            priceProvider.AddSpecialPrice(Banana.BananaCode, 3, 4.6m);

            foreach (var pp in priceProvider.Specials)
            {
                if (pp.Key.Product == Apple.AppleCode)
                {
                    Assert.Equal(1m, pp.Value);
                    Assert.Equal(2, pp.Key.Quantity);
                }
                else if (pp.Key.Product == Banana.BananaCode)
                {
                    Assert.Equal(4.6m, pp.Value);
                    Assert.Equal(3, pp.Key.Quantity);
                }
            }
        }
    }
}
