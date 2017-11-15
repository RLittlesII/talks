using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mediator.Demo.Extensions;
using MediatR;

namespace Mediator.Demo
{
    public class EmailContractorOffenderNotificationHandler : IAsyncNotificationHandler<ContractorOffenderNotification>
    {
        private readonly IEmailService _emailService;

        public EmailContractorOffenderNotificationHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(ContractorOffenderNotification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            await _emailService.SendEmail(notification.CreateEmail());
        }
    }
}