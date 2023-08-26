using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class Cover
    {
        public string Code { get; set; }
        public decimal Price { get; set; }

        public Cover(string code, decimal price)
        {
            Code = code;
            Price = price;
        }

    }
}
