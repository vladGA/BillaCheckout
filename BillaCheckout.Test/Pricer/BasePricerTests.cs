using BillaCheckout.Core;
using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.StoreProduct;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BillaCheckout.Test.Pricer
{
    public class BasePricerTests
    {
        [Fact(DisplayName = "Check if price method called")]
        public void CheckIfPriceCalled()
        {
            var basePricer = new Mock<BasePricer>()
            {
                CallBase = true
            };
            var cart = new Mock<ICart>();
            cart.Setup(x => x.GetProducts()).Returns(new List<Product>());

            basePricer.Object.PriceCart(cart.Object);
            basePricer.Verify(m => m.Price(It.IsAny<ICollection<PriceableBasket>>()), Times.Once);
        }

        [Fact(DisplayName = "Check if init method retrieves correct cart products count")]
        public void CheckInitMethod()
        {
            var basePricer = new Mock<BasePricer>()
            {
                CallBase = true
            };
            var cart = new Mock<ICart>();
            var products = new List<Product> { new Apple(), new Banana() };
            cart.Setup(x => x.GetProducts()).Returns(products);

            basePricer.Object.PriceCart(cart.Object);
            basePricer.Verify(m => m.Price(It.Is<ICollection<PriceableBasket>>(arg => arg.Count == products.Count)));
        }
    }
}
