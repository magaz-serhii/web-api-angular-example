using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Models.Manufacturers;
using Products.WebApi.Services;

namespace Products.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly ManufacturersService _manufacturersService;

        public ManufacturersController(ManufacturersService manufacturersService)
        {
            this._manufacturersService = manufacturersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetManufacturers()
        {
            var response = await this._manufacturersService.GetManufacturersAsync();
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetManufacturer(long id)
        {
            var result = await this._manufacturersService.GetManufacturerByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Manufacturer {id} not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturer(CreateManufacturerMadel model)
        {
            var result = await this._manufacturersService.CreateManufacturerAsync(model);
            return CreatedAtAction(nameof(GetManufacturer), new { id = result.Id }, result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateManufacturer(long id, UpdateManufacturerMadel model)
        {
            await this._manufacturersService.UpdateManufacturerAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteManufacturer(long id)
        {
            await this._manufacturersService.DeleteManufacturerAsync(id);
            return NoContent();
        }
    }

}
