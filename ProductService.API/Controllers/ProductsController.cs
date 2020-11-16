using Microsoft.AspNetCore.Mvc;
using ProductService.API.Infrastructure;
using ProductService.Core.Models;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = new Product[] {
                new Product { Id = 1, ProductCode = "PR01" },
                new Product { Id = 2, ProductCode = "PR02" },
                new Product { Id = 3, ProductCode = "PR03" }
            };

            var response = new ApiResponse<Product[]>() { Data = products, Message = "Products listed", Success = products.Length > 0 };

            return Ok(response);
        }

        // GET api/<ProductsController>/1
        [HttpGet("{id}")]
        public ApiResponse<Product> Get(int id)
        {
            var product = new Product { Id = 1, ProductCode = "PR01" };

            var response = new ApiResponse<Product>() { Data = product, Message = "Product listed", Success = product != null };

            return response;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ApiResponse<Product> Post([FromBody] Product value)
        {
            var product = new Product() { Id = 4, ProductCode = value.ProductCode };

            var response = new ApiResponse<Product>() { Data = product, Message = "Product added", Success = product != null };

            return response;
        }
    }
}
