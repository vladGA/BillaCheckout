using BillaCheckout.Core.StoreProduct;

namespace BillaCheckout.Core
{
    public interface ICheckout
    {
        void Scan(Product item);

        decimal Total();
    }
}
