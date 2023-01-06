using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoReactive.Services
{
    public class DataServiceMock<T> : IDataService<T>
        where T : Dto
    {
        public IEnumerable<T> Items { get; set; }

        protected DataServiceMock()
        {
            Items = new List<T>();
        }

        public Task<IEnumerable<T>> Get() => Task.FromResult(Items);

        public Task<T> Get(string id) => Task.FromResult(Items.FirstOrDefault(x => x.Id == id));
    }
}