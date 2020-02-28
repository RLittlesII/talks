using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace GoReactive.Search
{
    public partial class SearchList : ContentPageBase<SearchListViewModel>
    {
        public SearchList()
        {
            InitializeComponent();

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

            var textChangedEvents = Observable
                .FromEvent<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(eventHandler =>
                    {
                        void Handler(object sender, TextChangedEventArgs args) => eventHandler(args);
                        return Handler;
                    },
                    x => SearchBar.TextChanged += x,
                    x => SearchBar.TextChanged -= x);

            // textChangedEvents
            //     .Select(x => x.NewTextValue)
            //     .Throttle(TimeSpan.FromMilliseconds(750))
            //     .Where(x => !string.IsNullOrWhiteSpace(x))
            //     .DistinctUntilChanged()
            //     .InvokeCommand(this, x => x.ViewModel.Search);

            SearchBar
                .Events()
                .TextChanged
                .Select(x => x.NewTextValue)
                .Throttle(TimeSpan.FromMilliseconds(750))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .DistinctUntilChanged()
                .InvokeCommand(this, x =>  x.ViewModel.Search);
        }
    }
}
