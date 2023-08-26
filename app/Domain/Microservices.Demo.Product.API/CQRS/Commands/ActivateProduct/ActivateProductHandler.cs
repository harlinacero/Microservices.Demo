using MediatR;
using Microservices.Demo.Product.API.Domain;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.ActivateProduct
{
    public class ActivateProductHandler : IRequestHandler<ActivateProductCommand, ActivateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDomainService _productDomainService;

        public ActivateProductHandler(IProductRepository productRepository,ProductDomainService productDomainService)
        {
            _productRepository = productRepository;
            _productDomainService = productDomainService;
        }

        public async Task<ActivateProductResult> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.ProductId);
            product = _productDomainService.Activate(product);
            return new ActivateProductResult
            {
                ProductId = product.ProductStatusId
            };
        }
    }
}
