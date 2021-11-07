using BillaCheckout.Core;
using BillaCheckout.Core.StoreProduct;
using System.Linq;
using Xunit;

namespace BillaCheckout.Test
{
    public class CartTests
    {
        [Fact(DisplayName = "Check if products are added to cart")]
        public void CheckCartAdd()
        {
            var cart = new Cart();
            cart.AddProduct(new Apple());
            cart.AddProduct(new Pineapple());
            cart.AddProduct(new Pineapple());

            Assert.Equal(3, cart.GetProducts().Count);
            Assert.Equal(1, cart.GetProducts().Count(s => s.Code == Apple.AppleCode));
            Assert.Equal(2, cart.GetProducts().Count(s => s.Code == Pineapple.PineappleCode));
        }
    }
}
