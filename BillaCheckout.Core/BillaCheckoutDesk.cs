using BillaCheckout.Core.StoreProduct;

namespace BillaCheckout.Core
{
    public class BillaCheckoutDesk : ICheckout
    {
        private readonly ICart _cart;

        private readonly ICartPricer _cartPricer;

        public BillaCheckoutDesk(ICart cart, ICartPricer cartPricer)
        {
            _cart = cart;
            _cartPricer = cartPricer;
        }

        public void Scan(Product item)
        {
            _cart.AddProduct(item);
        }

        public decimal Total()
        {
            return _cartPricer.PriceCart(_cart);
        }
    }
}
