using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Web;
using DynamicData;
using Microsoft.AspNetCore.Http.Extensions;
using ReactiveUI;
using Rocket.Surgery.Airframe.Data.DuckDuckGo;

namespace Bang.Pages
{
    public class SearchViewModel : ReactiveObject
    {
        private readonly IDuckDuckGoService _duckDuckGoService;
        private string _searchText;
        private IEnumerable<RelatedTopic> _searchResults = Enumerable.Empty<RelatedTopic>();
        private ObservableAsPropertyHelper<bool> _canClear;

        public SearchViewModel(IDuckDuckGoService duckDuckGoService)
        {
            _duckDuckGoService = duckDuckGoService;

            this.WhenAnyValue(x => x.SearchText)
                .Where(string.IsNullOrEmpty)
                .Skip(1)
                .Select(_ => Enumerable.Empty<RelatedTopic>())
                .ToObservableChangeSet(x => x.FirstUrl)
                .DefaultIfEmpty()
                .ToCollection()
                .BindTo(this, x => x.SearchResults);

            this.WhenAnyValue(x => x.SearchText)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim())
                .Select(Uri.EscapeDataString)
                .SelectMany(ExecuteSearch)
                .ToCollection()
                .BindTo(this, x => x.SearchResults);

            Clear = ReactiveCommand.Create(() => { }, this.WhenAnyValue(x => x.SearchText).Select(x => !string.IsNullOrEmpty(x)));

            Clear
                .Subscribe(_ => SearchText = string.Empty);

            Clear.CanExecute
                .ToProperty(this, nameof(CanClear), out _canClear);
        }

        public ReactiveCommand<Unit,Unit> Clear { get; set; }

        public IEnumerable<RelatedTopic> SearchResults
        {
            get => _searchResults;
            set => this.RaiseAndSetIfChanged(ref _searchResults, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public bool CanClear => _canClear.Value;

        private IObservable<IChangeSet<RelatedTopic, string>> ExecuteSearch(string arg) =>
            _duckDuckGoService.Query(arg);
    }
}