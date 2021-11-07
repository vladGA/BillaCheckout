using BillaCheckout.Core.Pricer;
using BillaCheckout.Core.StoreProduct;
using System;
using System.Collections.Generic;
using Xunit;

namespace BillaCheckout.Test.Pricer
{
    public class PriceableBasketTests
    {
        [Fact(DisplayName = "Check if single product constructor succeeds")]
        public void CheckProductConstructor()
        {
            var product = new Apple();
            var basket = new PriceableBasket(product);
            Assert.NotNull(basket);
        }

        [Fact(DisplayName = "Check single null product constructor throws")]
        public void CheckNullProductConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new PriceableBasket((Product)null));
        }

        [Fact(DisplayName = "Check price and null items constructor throws")]
        public void CheckPriceAndItemsConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new PriceableBasket(0m, (ICollection<Product>)null));
        }

        [Fact(DisplayName = "Check price and empty items constructor throws")]
        public void CheckPriceAndEpmtyItemsConstructor()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ICollection<Product> prodcuts = new List<Product>();
                return new PriceableBasket(0m, prodcuts);
            });
        }

        [Fact(DisplayName = "Check first product retrival succeeds")]
        public void CheckFirstProduct()
        {
            var product = new Apple();
            var basket = new PriceableBasket(0m, product);
            Assert.Equal(product, basket.FirstProduct);
        }

        [Fact(DisplayName = "Check has single product")]
        public void CheckSingleProduct()
        {
            var apple = new Apple();
            var banana = new Banana();
            var singleBasket = new PriceableBasket(0m, new List<Product> { apple });
            var multiBasket = new PriceableBasket(0m, new List<Product> { apple, banana });

            Assert.True(singleBasket.HasSingleProduct);
            Assert.False(multiBasket.HasSingleProduct);
        }

        [Fact(DisplayName = "Check has single product")]
        public void CheckItems()
        {
            var apple = new Apple();
            var banana = new Banana();
            var singleBasket = new PriceableBasket(0m, new List<Product> { apple });
            var multiBasket = new PriceableBasket(0m, new List<Product> { apple, banana });

            Assert.True(singleBasket.HasSingleProduct);
            Assert.False(multiBasket.HasSingleProduct);
        }

        [Fact(DisplayName = "Check if products are added to basket")]
        public void CheckAddBasketItems()
        {
            var apple = new Apple();
            var banana = new Banana();
            var multiBasket = new PriceableBasket(0m, new List<Product> { apple, banana });
            multiBasket.AddToBasket(apple);
            multiBasket.AddToBasket(apple);
            multiBasket.AddToBasket(banana);

            Assert.Equal(5, multiBasket.Items.Count);
        }
    }
}
