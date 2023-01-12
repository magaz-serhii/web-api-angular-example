namespace Products.WebApi.Models.Products
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long ManufacturerId { get; set; }
        public long CategoryId { get; set; }
    }
}
