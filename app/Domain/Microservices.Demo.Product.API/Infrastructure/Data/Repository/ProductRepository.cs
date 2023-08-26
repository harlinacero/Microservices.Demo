using Microservices.Demo.Product.API.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Repository
{
    using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ProductRepository:IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
        }

        public async Task<Product> Add(Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            return product;
        }

        public async Task<List<Product>> FindAllActive()
        {
            return await _productDbContext
                .Products
                .Include(c => c.Covers)
                .Include("Questions.Choices")
                .Where(p => p.ProductStatusId == (int)EnumProductStatus.Active)
                .ToListAsync();
        }

        public async Task<Product> FindOne(string productCode)
        {
            return await _productDbContext
                .Products
                .Include(c => c.Covers)
                .Include("Questions.Choices")
                .Where(p => p.Code == productCode)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> FindById(int productId)
        {
            return await _productDbContext.Products.Include(c => c.Covers).Include("Questions.Choices")
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }
    }
}
