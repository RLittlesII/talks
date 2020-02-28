using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GoReactive.Services.Client;

namespace GoReactive.Services.Orders
{
    public class OrderClientMock : HubClientBase, IOrderClient
    {
        private static readonly Random Random = new Random();
        private static readonly string[] SampleNames = { "Rodney", "Cameron", "Alec", "Rod", "Glenn", "Mitchell", "David", "Scott" };
        private static readonly string[] SampleDrinks = { "Chai Tea Latte", "Blond Flat White", "Cappuccino", "Latte", "Dark Roast" };
        private static readonly DrinkSize[] Sizes = {DrinkSize.Small, DrinkSize.Medium, DrinkSize.Large, DrinkSize.ExtraLarge};

        public OrderClientMock()
        {
            Orders = Observable
                .Interval(TimeSpan.FromMilliseconds(1750))
                .Select(x => GenerateOrderDetail());
        }

        public override Task Connect() => Task.CompletedTask;

        public override Task<T> InvokeAsync<T>(string methodName) => Task.FromResult(default(T));

        public IObservable<OrderDetailDto> Orders { get; }
        
        private static OrderDetailDto GenerateOrderDetail() =>
            new OrderDetailDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = GenerateOrderName(),
                DrinkName = GenerateDrink(),
                Details = "None",
                Size = GenerateSize(),
                OrderTime = DateTime.Now
            };

        private static DrinkSize GenerateSize() => Sizes[Random.Next(0, Sizes.Length)];

        private static string GenerateDrink() => SampleDrinks[Random.Next(0, SampleDrinks.Length)];

        private static string GenerateOrderName() => SampleNames[Random.Next(0, SampleNames.Length)];

    }
}