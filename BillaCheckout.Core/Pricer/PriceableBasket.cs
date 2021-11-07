using System;
using System.Linq;
using System.Collections.Generic;
using BillaCheckout.Core.StoreProduct;
using BillaCheckout.Core.Util;

namespace BillaCheckout.Core.Pricer
{
    public class PriceableBasket : IPriceableBasket
    {
        public decimal Price { get; }

        public ICollection<Product> Items { get; }

        public bool HasSingleProduct { get => Items.Count == 1; }

        public Product FirstProduct { get => Items.First(); }

        public void AddToBasket(Product product)
        {
            Items.Add(product);
        }

        public PriceableBasket(Product item) : this(0m, item)
        { }

        public PriceableBasket(decimal price, Product item)
        {
            ArgumentValidation.EnsureNotNull(item, nameof(item));

            Price = price;
            Items = new List<Product> { item };
        }

        public PriceableBasket(ICollection<Product> items) : this(0, items)
        {
        }

        public PriceableBasket(decimal price, ICollection<Product> items)
        {
            ArgumentValidation.EnsureNotNull(items, nameof(items));

            if (items.Count == 0)
                throw new ArgumentException(nameof(items));

            Price = price;
            Items = new List<Product>(items);
        }
    }
}
