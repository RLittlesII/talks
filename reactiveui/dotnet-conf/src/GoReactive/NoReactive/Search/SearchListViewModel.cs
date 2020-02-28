using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GoReactive.Services;
using ReactiveUI;
using Xamarin.Forms;

namespace NoReactive.Search
{
    public class SearchListViewModel : ViewModelBase
    {
        private readonly IDuckDuckGoApi _duckDuckGoApi;
        private string _searchText;
        private IEnumerable<object> _searchResults;
        private bool _isRefreshing;

        public SearchListViewModel(IDuckDuckGoApi duckDuckGoApi)
        {
            _duckDuckGoApi = duckDuckGoApi;

            Search = new Command(async searchText => await ExecuteSearch((string)searchText));

            Refresh = new Command(async () => await ExecuteRefresh());
        }

        public ICommand Refresh { get; private set; }

        public ICommand Search { get; private set; }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public IEnumerable<object> SearchResults
        {
            get => _searchResults;
            set => this.RaiseAndSetIfChanged(ref _searchResults, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => this.RaiseAndSetIfChanged(ref _isRefreshing, value);
        }

        private async Task<object> ExecuteSearch(string searchText)
        {
            var result = await _duckDuckGoApi.Search(searchText);

            return result;
        }

        private async Task ExecuteRefresh()
        {
            IsRefreshing = true;

            await Task.CompletedTask;

            IsRefreshing = false;
        }
    }
}
