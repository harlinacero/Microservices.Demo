using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Entities
{
    public partial class Cover
    {
        public Cover()
        { }

        public Cover(string code, string name, string description, bool optional, decimal? sumInsured)
        {
            Code = code;
            Name = name;
            Description = description;
            Optional = optional;
            SumInsured = sumInsured;
        }
    }
}
