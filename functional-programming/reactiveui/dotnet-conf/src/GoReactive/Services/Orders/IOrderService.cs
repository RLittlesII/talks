using System;
using System.Threading.Tasks;
using DynamicData;

namespace GoReactive.Services.Orders
{
    public interface IOrderService : IDisposable
    {
        /// <summary>
        /// Gets an observable cache of orders.
        /// </summary>
        IObservableCache<OrderDetailDto, string> Orders { get; }

        /// <summary>
        /// Gets orders from the client.
        /// </summary>
        Task GetOrders();
    }
}