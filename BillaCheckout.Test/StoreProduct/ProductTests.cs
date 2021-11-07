using BillaCheckout.Core.StoreProduct;
using Xunit;

namespace BillaCheckout.Test.StoreProduct
{
    public class ProductTests
    {
        [Fact(DisplayName = "Check codes product")]
        public void ProductCodeTest()
        {
            Assert.Equal(Apple.AppleCode, new Apple().Code);
            Assert.Equal(Pineapple.PineappleCode, new Pineapple().Code);
            Assert.Equal(Banana.BananaCode, new Banana().Code);
            Assert.Equal(Orange.OrangeCode, new Orange().Code);
        }
    }
}
