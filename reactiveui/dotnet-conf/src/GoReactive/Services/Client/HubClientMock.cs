using System.Threading.Tasks;

namespace GoReactive.Services.Client
{
    public class HubClientMock : HubClientBase
    {
        public override Task Connect() => Task.CompletedTask;

        public override Task<T> InvokeAsync<T>(string methodName) => Task.FromResult(default(T));
    }
}