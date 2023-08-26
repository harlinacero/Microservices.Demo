using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicyDetails
{
    public class GetPolicyDetailsQuery : IRequest<GetPolicyDetailsQueryResult>
    {
        public string PolicyNumber { get; set; }
    }
}
