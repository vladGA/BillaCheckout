using System.Collections.Generic;
using BillaCheckout.Core;
using BillaCheckout.Core.Pricer.PricerStrategy;
using BillaCheckout.Core.Pricer.PricerStrategy.PerProduct;
using BillaCheckout.Core.Pricer.PricerStrategy.SingleSpecial;
using BillaCheckout.Core.Pricer.SingleChain;
using BillaCheckout.Core.StoreProduct;
using Microsoft.Extensions.DependencyInjection;

namespace BillaCheckout.Console
{
    public class Program
    {
        private static void RegisterPrices(ServiceCollection collection)
        {
            collection.AddSingleton<IProductUnitPriceProvider>(new InMemoryPriceProvider(
                new Dictionary<string, decimal>
                {
                    {Apple.AppleCode, 0.5m },
                    {Banana.BananaCode, 0.7m },
                    {Orange.OrangeCode, 0.45m },
                })
            );

            collection.AddSingleton<ISingleSpecialPriceProvider>(new InMemorySingleSpecialPriceProvider(
                new Dictionary<(string Product, int Quantity), decimal>
                {
                    {(Orange.OrangeCode, 3), 0.9m },
                    {(Banana.BananaCode, 2), 1m },
                }));
        }

        private static ServiceProvider RegisterServices()
        {
            var collection = new ServiceCollection();
            RegisterPrices(collection);
            collection.AddSingleton(sp =>
            {
                IPricerStrategyFactory strategyFactory = sp.GetRequiredService<IPricerStrategyFactory>();
                return strategyFactory.GetPricerStrategies();
            });

            collection.AddTransient<ICart, Cart>();
            collection.AddTransient<IPricerStrategyFactory, RegularStrategyFactory>();
            collection.AddTransient<ICartPricer, SingleChainPricer>();
            collection.AddTransient<ICheckout, BillaCheckoutDesk>();

            return collection.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var serviceProvider = RegisterServices();

            Test1(serviceProvider);
            Test2(serviceProvider);

            System.Console.ReadLine();
        }

        private static void Test1(ServiceProvider serviceProvider)
        {
            System.Console.WriteLine("***** Test1 *****");
            var checkout = serviceProvider.GetService<ICheckout>();
            checkout.Scan(new Apple());
            checkout.Scan(new Apple());
            checkout.Scan(new Apple());
            checkout.Scan(new Orange());
            checkout.Scan(new Orange());
            checkout.Scan(new Orange());
            checkout.Scan(new Orange());
            checkout.Scan(new Banana());
            checkout.Scan(new Banana());

            System.Console.WriteLine("3 apples, 4 oranges, 2 bananas costs: " + checkout.Total());
            System.Console.WriteLine("**************");
        }

        private static void Test2(ServiceProvider serviceProvider)
        {
            System.Console.WriteLine("***** Test2 *****");
            var checkout = serviceProvider.GetService<ICheckout>();
            checkout.Scan(new Apple());
            checkout.Scan(new Orange());
            checkout.Scan(new Banana());
            checkout.Scan(new Banana());
            checkout.Scan(new Banana());
            checkout.Scan(new Banana());
            checkout.Scan(new Banana());

            System.Console.WriteLine("1 apple, 1 orange, 5 bananas costs: " + checkout.Total());
            System.Console.WriteLine("**************");
        }

    }
}
