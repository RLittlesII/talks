using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Demo.Extensions
{
    public static class ColleagueExtensions
    {
        public static ContractorOffenderNotification Notification(this GetContractorResponse response)
        {
            return new ContractorOffenderNotification();
        }
    }
}