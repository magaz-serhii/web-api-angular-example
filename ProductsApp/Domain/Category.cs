namespace Domain
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }

        public IList<Manufacturer> Manufacturers { get; set; }
    }
}
