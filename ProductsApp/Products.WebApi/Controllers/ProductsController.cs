using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Models.Products;
using Products.WebApi.Services;

namespace Products.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            this._productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await this._productsService.GetProductsAsync();
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var result = await this._productsService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel model)
        {
            var result = await this._productsService.CreateProductAsync(model);
            return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, UpdateProductModel model)
        {
            await this._productsService.UpdateProductAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await this._productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
