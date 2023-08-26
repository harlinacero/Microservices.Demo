using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Repository
{
    using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
    public interface IProductRepository
    {
        Task<Product> Add(Product product);

        Task<List<Product>> FindAllActive();

        Task<Product> FindOne(string productCode);

        Task<Product> FindById(int productId);
    }
}
