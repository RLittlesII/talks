using System;
using System.Threading.Tasks;

namespace LoginState.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task Authenticate(string username, string password) =>
            await Task.Delay(TimeSpan.FromSeconds(5));
    }
}