using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Mediator.Demo
{
    public class GetContractorRequestHandler : IAsyncRequestHandler<GetContractorRequest, GetContractorResponse>
    {
        private readonly IContractorRepository _contractorRepository;

        public GetContractorRequestHandler(IContractorRepository contractorRepository)
        {
            _contractorRepository = contractorRepository;
        }

        public Task<GetContractorResponse> Handle(GetContractorRequest message)
        {
            if (message == null)
            {
                throw  new ArgumentNullException(nameof(message));
            }
            
            if (message.FirstName == null)
            {
                throw new ArgumentNullException(nameof(message.FirstName));
            }

            if (message.LastName == null)
            {
                throw new ArgumentNullException(nameof(message.LastName));
            }

            if (message.DateOfBirth == null)
            {
                throw new ArgumentNullException(nameof(message.DateOfBirth));
            }

            var matches =
                _contractorRepository.GetAll()
                    .Where(x => x.FirstName == message.FirstName)
                    .Where(x => x.LastName == message.LastName)
                    .Where(x => x.DateOfBirth == message.DateOfBirth).Select(x => new ContractorViewModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        DateOfBirth = x.DateOfBirth,
                        PossibleMatch = true
                    });

            return Task.FromResult(new GetContractorResponse
            {
                ViewModels = matches
            });
        }
    }

    public class ContractorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public bool PossibleMatch { get; set; }
    }

    public class GetContractorRequest : BaseRequest<GetContractorResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string IDNumber { get; set; }
    }

    public class GetContractorResponse : BaseResponse
    {
        public IEnumerable<ContractorViewModel> ViewModels { get; set; }
    }
}