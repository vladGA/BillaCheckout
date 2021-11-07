using System.Collections.Generic;
using BillaCheckout.Core.StoreProduct;

namespace BillaCheckout.Core.Pricer.PricerStrategy.Aggregation
{
    public class AllSumStrategy : IPricerStrategy
    {
        public ICollection<PriceableBasket> Price(ICollection<PriceableBasket> basketPrices)
        {
            var allProducts = new List<Product>();
            var totalPrice = 0m;
            foreach (var basket in basketPrices)
            {
                allProducts.AddRange(basket.Items);
                totalPrice += basket.Price;
            }

            var totalBasket = new PriceableBasket(totalPrice, allProducts);

            return new List<PriceableBasket>
            {
                totalBasket
            };
        }
    }
}
