using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace LoginState.Services
{
    public class ConnectivityService : IConnectivity
    {
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public ConnectivityService()
        {
            ConnectionStateChanges =
                Observable
                    .FromEvent<EventHandler<ConnectivityChangedEventArgs>, ConnectivityChangedEventArgs>(eventHandler =>
                        {
                            void Handler(object sender, ConnectivityChangedEventArgs eventArgs) =>
                                eventHandler(eventArgs);
                            return Handler;
                        },
                        x => Connectivity.ConnectivityChanged += x,
                        x => Connectivity.ConnectivityChanged -= x);
        }

        public IObservable<ConnectivityChangedEventArgs> ConnectionStateChanges { get; }
    }
}