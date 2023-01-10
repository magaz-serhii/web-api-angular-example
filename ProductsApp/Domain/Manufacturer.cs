namespace Domain
{
    public class Manufacturer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
