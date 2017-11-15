using System;
using System.Globalization;

namespace Mediator.Demo
{
    public class AuthorizationToken
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public string ConnectionString { get; set; }

        public string ConnectionStringInContext { get; set; }

        public CultureInfo CultureInfo { get; set; }

        public string DefaultTimeZone { get; set; }
    }
}