using System;
using System.Collections.Generic;
using System.Linq;

namespace BillaCheckout.Core.Pricer.PricerStrategy.PerProduct
{
    public class PerProductStrategy : IPricerStrategy
    {
        private readonly IProductUnitPriceProvider _priceProvider;

        public PerProductStrategy(IProductUnitPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
        }

        public ICollection<PriceableBasket> Price(ICollection<PriceableBasket> basketPrices)
        {
            var processedPrices = new List<PriceableBasket>();

            foreach (var basketPrice in basketPrices)
            {
                if (basketPrice.Items.Count > 1)
                {
                    //Skip already sliced and diced product baskets
                    processedPrices.Add(basketPrice);
                    continue;
                }

                var product = basketPrice.Items.First();
                if (!_priceProvider.ProductPrices.ContainsKey(product.Code))
                    throw new Exception("Unsupported product");

                processedPrices.Add(new PriceableBasket(_priceProvider.ProductPrices[product.Code], product));
            }

            return processedPrices;
        }
    }
}
