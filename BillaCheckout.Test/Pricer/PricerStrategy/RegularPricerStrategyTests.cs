using BillaCheckout.Core.Pricer.PricerStrategy;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;
using Moq;
using Xunit;

namespace BillaCheckout.Test.Pricer.PricerStrategy
{
    public class RegularPricerStrategyTests
    {
        private readonly Mock<IProductUnitPriceProvider> _unitPriceProvider;
        private readonly Mock<ISingleSpecialPriceProvider> _singleSpecialPriceProvider;
        public RegularPricerStrategyTests()
        {
            _unitPriceProvider = new Mock<IProductUnitPriceProvider>();
            _singleSpecialPriceProvider = new Mock<ISingleSpecialPriceProvider>();

        }

        [Fact(DisplayName = "Check if there are 3 strategies registered")]
        public void CheckStrategyCount()
        {
            var strategyFactory = new
                RegularStrategyFactory(_unitPriceProvider.Object, _singleSpecialPriceProvider.Object);

            Assert.Equal(3, strategyFactory.GetPricerStrategies().Count);
        }
    }
}
