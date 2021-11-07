using System.Collections.Generic;
using System.Linq;

namespace BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial
{
    public class SingleSpecialStrategy : IPricerStrategy
    {
        private readonly ISingleSpecialPriceProvider _priceProvider;

        public SingleSpecialStrategy(ISingleSpecialPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
        }

        public ICollection<PriceableBasket> Price(ICollection<PriceableBasket> basketPrices)
        {
            var nextPrices = basketPrices;
            foreach (var special in _priceProvider.Specials)
            {
                nextPrices = ApplySpecial(nextPrices, special.Key.Product,
                    special.Key.Quantity, special.Value);
            }

            return nextPrices;
        }

        private ICollection<PriceableBasket> ApplySpecial(ICollection<PriceableBasket> basketPrices,
            string productCode, int quantity, decimal price)
        {
            var result = new List<PriceableBasket>();
            var specialProducts = new List<PriceableBasket>();

            foreach (var basketPrice in basketPrices)
            {
                if (!basketPrice.HasSingleProduct || basketPrice.FirstProduct.Code != productCode)
                    result.Add(basketPrice);
                else
                    specialProducts.Add(basketPrice);
            }


            ApplyRuleToSpecialProduct(quantity, price, specialProducts, result);

            return result;

        }

        private static void ApplyRuleToSpecialProduct(int quantity, decimal price,
            List<PriceableBasket> specialProducts, List<PriceableBasket> result)
        {
            var currentGroupCount = 0;
            var currentBuffer = new List<PriceableBasket>();
            foreach (var sp in specialProducts)
            {
                ++currentGroupCount;
                currentBuffer.Add(sp);

                if (currentGroupCount != quantity) continue;

                result.Add(new PriceableBasket(price, currentBuffer.Select(p => p.FirstProduct).ToList()));
                currentBuffer = new List<PriceableBasket>();
                currentGroupCount = 0;
            }

            if (currentBuffer.Count > 0)
            {
                result.AddRange(currentBuffer);
            }
        }
    }
}
