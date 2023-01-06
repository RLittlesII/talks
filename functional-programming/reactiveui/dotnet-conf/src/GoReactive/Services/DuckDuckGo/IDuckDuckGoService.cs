using System;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;

namespace GoReactive.Services
{
    public interface IDuckDuckGoService
    {
        Task Query(string query);

        IObservable<IChangeSet<DuckDuckGoQueryResult, string>> QueryResults { get; }
    }
}