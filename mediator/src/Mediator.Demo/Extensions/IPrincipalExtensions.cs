using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Mediator.Demo.Extensions
{
    public static class IPrincipalExtensions
    {
        public static AuthorizationToken Tokenize(this IPrincipal principal)
        {
            return new AuthorizationToken
            {
            };
        }
    }
}