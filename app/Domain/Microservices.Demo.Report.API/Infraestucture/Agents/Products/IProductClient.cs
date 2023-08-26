using Microservices.Demo.Report.API.Domain.Entities;
using RestEase;

namespace Microservices.Demo.Report.API.Infraestucture.Agents.Products
{
    public interface IProductClient
    {
        [Get]
        Task<IEnumerable<Product>> GetAsync();
    }
}
