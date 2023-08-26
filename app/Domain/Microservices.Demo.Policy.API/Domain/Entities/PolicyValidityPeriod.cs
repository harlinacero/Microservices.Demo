using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Entities
{
    public partial class PolicyValidityPeriod
    {
        public PolicyValidityPeriod(DateTime policyFrom, DateTime policyTo)
        {
            PolicyFrom = policyFrom;
            PolicyTo = policyTo;
        }
        public PolicyValidityPeriod Clone()
        {
            return new PolicyValidityPeriod(PolicyFrom, PolicyTo);
        }
        public bool Contains(DateTime theDate)
        {
            if (theDate > PolicyTo)
                return false;

            if (theDate < PolicyFrom)
                return false;

            return true;
        }
    }
}
