using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.DiscontinueProduct
{
    public class DiscontinueProductCommand : IRequest<DiscontinueProductResult>
    {
        public int ProductId { get; set; }
    }
}
