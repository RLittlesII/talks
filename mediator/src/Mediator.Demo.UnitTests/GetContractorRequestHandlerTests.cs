using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Mediator.Demo.UnitTests
{
    public class GetContractorRequestHandlerTests
    {
        [Fact]
        public async Task Should_Throw_If_Null()
        {
            // Given
            var contractorRepository = Substitute.For<IContractorRepository>();
            var fixture = new GetContractorRequestHandler(contractorRepository);

            // When
            var result = await Record.ExceptionAsync(async () => await fixture.Handle(null));

            // Then
            Assert.IsType<ArgumentNullException>(result);
        }

        [Fact]
        public async Task Should_Throw_If_First_Name_Null()
        {
            // Given
            var contractorRepository = Substitute.For<IContractorRepository>();
            var fixture = new GetContractorRequestHandler(contractorRepository);

            // When
            var result = await Record.ExceptionAsync(async () => await fixture.Handle(new GetContractorRequest { FirstName = null }));

            // Then
            Assert.IsType<ArgumentNullException>(result);
        }

        [Fact]
        public async Task Should_Throw_If_Last_Name_Null()
        {
            // Given
            var contractorRepository = Substitute.For<IContractorRepository>();
            var fixture = new GetContractorRequestHandler(contractorRepository);

            // When
            var result = await Record.ExceptionAsync(async () => await fixture.Handle(new GetContractorRequest { LastName = null }));

            // Then
            Assert.IsType<ArgumentNullException>(result);
        }

        [Fact]
        public async Task Should_Throw_If_Date_Of_Birth_Null()
        {
            // Given
            var contractorRepository = Substitute.For<IContractorRepository>();
            var fixture = new GetContractorRequestHandler(contractorRepository);

            // When
            var result = await Record.ExceptionAsync(async () => await fixture.Handle(new GetContractorRequest { DateOfBirth = DateTimeOffset.UtcNow }));

            // Then
            Assert.IsType<ArgumentNullException>(result);
        }

        [Fact]
        public async Task Should_Return_Offender()
        {
            // Given
            var contractorRepository = new ContractorRepository();
            var fixture = new GetContractorRequestHandler(contractorRepository);

            // When
            var result = await fixture.Handle(new GetContractorRequest
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTimeOffset(new DateTime(1966, 10, 15))
            });

            // Then
            Assert.NotEmpty(result.ViewModels);
            Assert.Contains(result.ViewModels,
                x =>
                    x.FirstName == "John" && x.LastName == "Doe" &&
                    x.DateOfBirth == new DateTimeOffset(new DateTime(1966, 10, 15)));
        }
    }
}