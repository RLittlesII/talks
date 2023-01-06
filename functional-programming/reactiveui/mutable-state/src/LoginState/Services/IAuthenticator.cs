using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginState.Services
{
    public interface IAuthenticator
    {
        Task Authenticate(string username, string password);
    }
}
