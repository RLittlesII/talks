using System;
using GoReactive.Services.Client;

namespace GoReactive.Services.Orders
{
    public interface IOrderClient : IHubClient
    {
        IObservable<OrderDetailDto> Orders { get; }
    }
}