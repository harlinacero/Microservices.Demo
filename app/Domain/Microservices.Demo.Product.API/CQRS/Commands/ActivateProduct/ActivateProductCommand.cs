using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.ActivateProduct
{
    public class ActivateProductCommand : IRequest<ActivateProductResult>
    {
        public int ProductId { get; set; }
    }
}
