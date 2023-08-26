using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Demo.Product.API.Application;
using Microservices.Demo.Product.API.CQRS.Commands.ActivateProduct;
using Microservices.Demo.Product.API.CQRS.Commands.CreateProduct;
using Microservices.Demo.Product.API.CQRS.Commands.DiscontinueProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductApplicationService _productApplicationService;
        public ProductsController(ProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }
        // GET api/products
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {            
            return new JsonResult(await _productApplicationService.GetAllAsync());
        }

        // GET api/products/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult> GetByCode([FromRoute]string code)
        {
            return new JsonResult(await _productApplicationService.GetByCodeAsync(code));
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult> PostDraft([FromBody] CreateProductDraftCommand command)
        {
            return new JsonResult(await _productApplicationService.CreateProductDraftAsync(command));
        }

        // POST api/products/activate
        [HttpPost("/activate")]
        public async Task<ActionResult> Activate([FromBody] ActivateProductCommand command)
        {
            return new JsonResult(await _productApplicationService.ActivateProductAsync(command));
        }

        // POST api/products/discontinue
        [HttpPost("/discontinue")]
        public async Task<ActionResult> Discontinue([FromBody] DiscontinueProductCommand command)
        {
            return new JsonResult(await _productApplicationService.DiscontinueProductAsync(command));
        }
    }
}