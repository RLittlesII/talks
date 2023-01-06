using System;
using System.Threading.Tasks;

namespace GoReactive.Services.Client
{
    public interface IHubClient : IDisposable
    {
        /// <summary>
        /// Connect to the Hub.
        /// </summary>
        /// <returns>A task to monitor the progress.</returns>
        Task Connect();

        /// <summary>
        /// Invokes a method with the provided name.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>A completion value.</returns>
        Task<T> InvokeAsync<T>(string methodName);
    }
}