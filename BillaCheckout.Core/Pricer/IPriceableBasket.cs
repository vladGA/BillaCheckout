using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;

namespace BillaCheckout.Core.Pricer
{
    public interface IPriceableBasket
    {
        decimal Price { get; }

        ICollection<Product> Items { get; }

        bool HasSingleProduct { get; }

        Product FirstProduct { get; }

        void AddToBasket(Product product);
    }
}
