using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Mediator.Demo.UnitTests
{
    public class ContractorOffenderNotificationHandlerTests
    {
        public class TheEmailHandler
        {
            [Fact]
            public async Task Should_Send_Email()
            {
                // Given
                var emailService = Substitute.For<IEmailService>();
                var fixture = new EmailContractorOffenderNotificationHandler(emailService);

                // When
                await fixture.Handle(new ContractorOffenderNotification());

                // Then
                await emailService.Received().SendEmail(Arg.Any<ContractorOffenderEmail>());
            }
        }

        public class TheLogHandler
        {
            [Fact]
            public async Task Should_Log()
            {
                // Given
                var logger = Substitute.For<ILogger>();
                var fixture = new LogContractorOffenderNotificationHandler(logger);

                // When
                await fixture.Handle(new ContractorOffenderNotification());

                // Then
                await logger.Received().Log(Arg.Any<string>());
            }
        }
    }
}