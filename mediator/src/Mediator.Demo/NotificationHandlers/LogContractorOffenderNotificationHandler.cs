using System.Threading.Tasks;
using MediatR;

namespace Mediator.Demo
{
    public class LogContractorOffenderNotificationHandler : IAsyncNotificationHandler<ContractorOffenderNotification>
    {
        private readonly ILogger _logger;

        public LogContractorOffenderNotificationHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(ContractorOffenderNotification notification)
        {
            _logger.Log($"Possible Offender: {notification.FirstName}{notification.LastName}");

            return Task.CompletedTask;
        }
    }
}