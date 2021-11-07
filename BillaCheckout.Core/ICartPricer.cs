namespace BillaCheckout.Core
{
    public interface ICartPricer
    {
        decimal PriceCart(ICart cart);
    }
}
