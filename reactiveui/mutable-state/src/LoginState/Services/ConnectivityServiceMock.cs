using System;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace LoginState.Services
{
    public class ConnectivityServiceMock : IConnectivity
    {
        public ConnectivityServiceMock()
        {
            ConnectionStateChanges =
                Observable
                    .Timer(TimeSpan.FromSeconds(5))
                    .Select(x =>
                        new ConnectivityChangedEventArgs(NetworkAccess.None, new[] { ConnectionProfile.Unknown }));
        }

        public IObservable<ConnectivityChangedEventArgs> ConnectionStateChanges { get; }
    }
}