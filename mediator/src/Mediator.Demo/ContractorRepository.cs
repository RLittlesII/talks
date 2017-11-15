using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Demo
{
    public class ContractorRepository : IContractorRepository
    {
        public IList<Contractor> Contractors { get; set; }

        public ContractorRepository()
        {
            Contractors = new List<Contractor>
            {
                new Contractor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTimeOffset(new DateTime(1966, 10, 15))
                },
                new Contractor
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = new DateTimeOffset(new DateTime(1966, 10, 15))
                },
                new Contractor
                {
                    Id = 3,
                    FirstName = "Damon",
                    LastName = "Wayans",
                    DateOfBirth = new DateTimeOffset(new DateTime(1966, 10, 15))
                }
            };
        }

        public IEnumerable<Contractor> GetAll()
        {
            return Contractors;
        }
    }

    public class Contractor
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
    }
}