using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Essentials;

namespace GoReactive.Load
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

            this.WhenAnyValue(x => x.ViewModel.Orders)
                .Where((x => x != null))
                .BindTo(this, x => x.LoadDataListView.ItemsSource)
                .DisposeWith(ViewBindings);
        }
    }
}
