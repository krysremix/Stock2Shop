using Microsoft.AspNetCore.Mvc;
using Stock2Shop.Core.Models;
using Stock2Shop.Core.Services;

namespace Stock2Shop.API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromBody] List<Product> products)
        {
            if (products == null || !products.Any())
                return BadRequest("Product list is empty or invalid.");

            try
            {
                await _service.AddProductsAsync(products);
                return Created("products", products);
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return Ok(products);
        }
    }
}
