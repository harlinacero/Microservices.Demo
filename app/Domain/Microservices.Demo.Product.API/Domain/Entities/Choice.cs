using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Entities
{
    public partial class Choice
    {
        public Choice()
        { }

        public Choice(string code, string label)
        {
            Code = code;
            Label = label;
        }
    }
}
