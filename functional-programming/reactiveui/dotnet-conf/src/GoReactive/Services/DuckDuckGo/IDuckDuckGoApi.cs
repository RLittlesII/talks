using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace GoReactive.Services
{
    public interface IDuckDuckGoApi
    {
        [Get("/?q={query}&format=json")]
        Task<DuckDuckGoSearchResult> Search(string query);
        
        [Get("/?q={query}&format=json")]
        Task<DuckDuckGoSearchResult> Search(string query, CancellationToken cancellationToken);
    }
}