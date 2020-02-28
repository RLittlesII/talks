using System;
using System.Threading.Tasks;

namespace GoReactive.Services.Client
{
    public abstract class HubClientBase : IHubClient
    {
        public abstract Task Connect();

        public abstract Task<T> InvokeAsync<T>(string methodName);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}