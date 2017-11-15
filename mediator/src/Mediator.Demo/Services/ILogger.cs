using System.Threading.Tasks;

namespace Mediator.Demo
{
    public interface ILogger
    {
        Task Log(string log);
    }
}