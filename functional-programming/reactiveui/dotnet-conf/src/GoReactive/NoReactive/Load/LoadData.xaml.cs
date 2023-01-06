using System;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace NoReactive.Load
{
    public partial class LoadData : ContentPageBase<LoadDataViewModel>
    {
        public LoadData()
        {
            InitializeComponent();

            var connected =
                Observable.FromEvent<EventHandler<ConnectivityChangedEventArgs>, ConnectivityChangedEventArgs>(
                    eventHandler =>
                    {
                        void Handler(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs) =>
                            eventHandler(connectivityChangedEventArgs);

                        return Handler;
                    },
                    x => Connectivity.ConnectivityChanged += x,
                    x => Connectivity.ConnectivityChanged += x);
        }
    }
}
