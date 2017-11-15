using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Demo
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(IEmail email)
        {
            return Task.CompletedTask;
        }
    }

    public class ContractorOffenderEmail : IEmail { }
}