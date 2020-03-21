using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicData;

namespace GoReactive.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderClient _client;
        private readonly SourceCache<OrderDetailDto, string> _orders = new SourceCache<OrderDetailDto, string>(x => x.Id);

        public OrderService(IOrderClient client)
        {
            _client = client;
            _client
                .Orders
                .Subscribe(detail => _orders.AddOrUpdate(detail));
        }

        public IObservableCache<OrderDetailDto, string> Orders => _orders;

        public async Task GetOrders()
        {
            var orderDetails = await _client.InvokeAsync<OrderDetailDto>("GetOrders").ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
                _orders?.Dispose();
            }
        }
    }
}