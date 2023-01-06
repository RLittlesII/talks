using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using DynamicData;

namespace GoReactive.Services
{
    public class DuckDuckGoService : IDuckDuckGoService
    {
        private readonly IDuckDuckGoApi _duckDuckGoApi;
        private readonly SourceCache<DuckDuckGoQueryResult, string> _queryResults =
            new SourceCache<DuckDuckGoQueryResult, string>(x => x.FirstUrl);

        public DuckDuckGoService(IDuckDuckGoApi duckDuckGoApi)
        {
            _duckDuckGoApi = duckDuckGoApi;
        }

        public async Task Query(string query)
        {
            var results = await _duckDuckGoApi.Search(query).ConfigureAwait(false);
            foreach (var relatedTopic in results.RelatedTopics.Where(x => !string.IsNullOrEmpty(x.FirstUrl)))
            {
                _queryResults.AddOrUpdate(relatedTopic);
            }
        }

        public IObservable<IChangeSet<DuckDuckGoQueryResult, string>> QueryResults =>
            _queryResults.Connect();
    }
}