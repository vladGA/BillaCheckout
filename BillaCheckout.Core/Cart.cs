using BillaCheckout.Core.StoreProduct;
using System.Collections.Generic;

namespace BillaCheckout.Core
{
    public class Cart : ICart
    {
        private readonly IList<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public ICollection<Product> GetProducts()
        {
            return _products;
        }
    }
}
