using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class Calculation
    {
        public string ProductCode { get; set; }
        public DateTimeOffset PolicyFrom { get; set; }
        public DateTimeOffset PolicyTo { get; set; }
        public decimal TotalPremium { get; set; }
        public Dictionary<string, Cover> Covers { get; set; }
        public Dictionary<string, object> Subject { get; set; }

        public Calculation()
        {
            Covers = new Dictionary<string, Cover>();
            Subject = new Dictionary<string, object>();
        }        
    }
}
