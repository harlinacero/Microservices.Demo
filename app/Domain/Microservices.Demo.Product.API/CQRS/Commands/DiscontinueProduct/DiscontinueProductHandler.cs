using MediatR;
using Microservices.Demo.Product.API.Domain;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.DiscontinueProduct
{
    public class DiscontinueProductHandler: IRequestHandler<DiscontinueProductCommand, DiscontinueProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDomainService _productDomainService;

        public DiscontinueProductHandler(IProductRepository productRepository, ProductDomainService productDomainService)
        {
            _productRepository = productRepository;
            _productDomainService = productDomainService;
        }

        public async Task<DiscontinueProductResult> Handle(DiscontinueProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.ProductId);
            product = _productDomainService.Discontinue(product);
            return new DiscontinueProductResult
            {
                ProductId = product.ProductId
            };
        }
    }
}
