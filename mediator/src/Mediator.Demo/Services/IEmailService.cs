using System.Threading.Tasks;

namespace Mediator.Demo
{
    public interface IEmailService
    {
        Task SendEmail(IEmail email);
    }

    public interface IEmail
    {
    }
}