using System.Reactive.Subjects;
using System.Threading.Tasks;
using GoReactive.Services.Client;
using GoReactive.Services.Orders;
using Microsoft.AspNetCore.SignalR.Client;

namespace GoReactive.Services.SignalR
{
    public class SignalRHubClient : HubClientBase
    {
        private readonly HubConnection _connection;
        private readonly ISubject<OrderDetailDto> _orderDetailDto = new Subject<OrderDetailDto>();

        public SignalRHubClient(string connectionString)
        {
            _connection = new HubConnectionBuilder().WithUrl(connectionString).Build();
            _connection.On<OrderDetailDto>("GetOrders", detail => _orderDetailDto.OnNext(detail));
        }

        public override async Task Connect() => await _connection.StartAsync().ConfigureAwait(false);

        public override async Task<T> InvokeAsync<T>(string methodName) =>
            await _connection.InvokeAsync<T>(methodName).ConfigureAwait(false);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.StopAsync();
                _connection?.DisposeAsync();
                _orderDetailDto?.OnCompleted();
            }
        }
    }
}