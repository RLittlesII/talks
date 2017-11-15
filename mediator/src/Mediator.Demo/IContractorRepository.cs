using System.Collections.Generic;

namespace Mediator.Demo
{
    public interface IContractorRepository
    {
        IEnumerable<Contractor> GetAll();
    }
}