using System;
using Xamarin.Essentials;

namespace LoginState.Services
{
    public interface IConnectivity
    {
        IObservable<ConnectivityChangedEventArgs> ConnectionStateChanges { get; }
    }
}