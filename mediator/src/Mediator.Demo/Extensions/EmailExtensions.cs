using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Demo.Extensions
{
    public static class EmailExtensions
    {
        public static ContractorOffenderEmail CreateEmail(this ContractorOffenderNotification contractorOffenderNotification)
        {
            return new ContractorOffenderEmail();
        }
    }
}