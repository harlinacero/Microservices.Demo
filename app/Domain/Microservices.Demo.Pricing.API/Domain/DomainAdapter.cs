using DynamicExpresso;
using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Domain
{
    public static class DomainAdapter
    {
        public static (List<Parameter>, List<object>) CalculationToCalculationParams(Calculation calculation)
        {
            var parameters = new List<Parameter>();
            var values = new List<object>();

            parameters.Add(new Parameter("policyFrom", typeof(DateTimeOffset)));
            values.Add(calculation.PolicyFrom);
            parameters.Add(new Parameter("policyTo", typeof(DateTimeOffset)));
            values.Add(calculation.PolicyTo);

            foreach (var cover in calculation.Covers)
            {
                parameters.Add(new Parameter(cover.Key, typeof(Cover)));
                values.Add(cover.Value);
            }

            foreach (var question in calculation.Subject)
            {
                parameters.Add(new Parameter(question.Key, question.Value.GetType()));
                values.Add(question.Value);
            }

            return (parameters, values);
        }
    }
}
