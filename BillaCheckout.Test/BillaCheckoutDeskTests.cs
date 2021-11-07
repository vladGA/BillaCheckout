using BillaCheckout.Core;
using BillaCheckout.Core.StoreProduct;
using Moq;
using Xunit;

namespace BillaCheckout.Test
{
    public class BillaCheckoutDeskTests
    {
        [Fact(DisplayName = "Check if Scan calls cart add product")]
        public void CheckIfScanAddsToCart()
        {
            var cart = new Mock<ICart>();
            var cartPricer = new Mock<ICartPricer>();

            var checkout = new BillaCheckoutDesk(cart.Object, cartPricer.Object);
            checkout.Scan(new Apple());
            checkout.Scan(new Apple());
            checkout.Scan(new Banana());

            cart.Verify(mock => mock.AddProduct(It.Is<Product>(s => s != null)), Times.Exactly(3));
        }

        [Fact(DisplayName = "Check if Total calls price cart")]
        public void CheckIfTotalCallsPriceCart()
        {
            var cart = new Mock<ICart>();
            var cartPricer = new Mock<ICartPricer>();

            var checkout = new BillaCheckoutDesk(cart.Object, cartPricer.Object);
            var total = checkout.Total();

            cartPricer.Verify(mock => mock.PriceCart(It.Is<ICart>(s => s != null)), Times.Once());
        }
    }
}
