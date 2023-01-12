using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Models.Categories;
using Products.WebApi.Services;

namespace Products.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            this._categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await this._categoriesService.GetCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCategory(long id)
        {
            var result = await this._categoriesService.GetCategoryByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Category {id} not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            var result = await this._categoriesService.CreateCategoryAsync(model);
            return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateCategory(long id, UpdateCategoryModel model)
        {
            await this._categoriesService.UpdateCategoryAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            await this._categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }

}
