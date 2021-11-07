using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;

namespace BillaCheckout.Core
{
    public interface ICart
    {
        void AddProduct(Product product);

        ICollection<Product> GetProducts();
    }
}