using Products.WebApi.DTO.Categories;
using Products.WebApi.DTO.Manufacturers;

namespace Products.WebApi.DTO.Products
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public CategoryDto Category { get; set; }
    }
}
