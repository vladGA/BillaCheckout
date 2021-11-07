using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer
{
    public abstract class BasePricer : ICartPricer
    {
        public decimal PriceCart(ICart cart)
        {
            return Price(Init(cart));
        }

        public abstract decimal Price(ICollection<PriceableBasket> basketPrices);

        private ICollection<PriceableBasket> Init(ICart cart)
        {
            var result = new List<PriceableBasket>();
            foreach (var product in cart.GetProducts())
            {
                result.Add(new PriceableBasket(product));
            }

            return result;
        }
    }
}
