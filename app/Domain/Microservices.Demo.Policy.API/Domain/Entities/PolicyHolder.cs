using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Entities
{
    public partial class PolicyHolder
    {
        public PolicyHolder(string firstName, string lastName, string pesel, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Address = address;

            Offers = new HashSet<Offer>();
            PolicyVersions = new HashSet<PolicyVersion>();
        }
    }
}
