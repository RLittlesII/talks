using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoReactive.Search
{
    public partial class SearchList : ContentPageBase<SearchListViewModel>
    {
        public SearchList()
        {
            InitializeComponent();

            #region bindings
            this.BindCommand(ViewModel, x => x.Refresh, x => x.SearchListView, nameof(SearchListView.Refreshing))
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.SearchListView.IsRefreshing)
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, x => x.CurrentResult.Url, x => x.SearchResult.Text)
                .DisposeWith(ViewBindings);

            this.WhenAnyValue(x => x.ViewModel.SearchResults)
                .Where(x => x != null)
                .BindTo(this, x => x.SearchListView.ItemsSource)
                .DisposeWith(ViewBindings);
            #endregion

            var textChangedEvents = Observable.FromEvent<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(eventHandler =>
                    {
                        void Handler(object sender, TextChangedEventArgs args) => eventHandler(args);
                        return Handler;
                    },
                    x => SearchBar.TextChanged += x,
                    x => SearchBar.TextChanged -= x);

            textChangedEvents
                .Throttle(TimeSpan.FromMilliseconds(750), RxApp.TaskpoolScheduler)
                .Select(x => x.NewTextValue.Trim())
                .DistinctUntilChanged()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Search)
                .DisposeWith(ViewBindings);

            SearchBar
                .Events()
                .TextChanged
                .Throttle(TimeSpan.FromMilliseconds(750), RxApp.TaskpoolScheduler)
                .Select(x => x.NewTextValue.Trim())
                .DistinctUntilChanged()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x =>  x.ViewModel.Search)
                .DisposeWith(ViewBindings);


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

            Events.ConnectivityConnectivityChanged.Subscribe();

        }
    }
}
