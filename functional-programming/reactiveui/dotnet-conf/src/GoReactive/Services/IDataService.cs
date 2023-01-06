using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoReactive.Services
{
    public interface IDataService<T>
        where T : Dto
    {
        Task<IEnumerable<T>> Get();

        Task<T> Get(string id);
    }
}