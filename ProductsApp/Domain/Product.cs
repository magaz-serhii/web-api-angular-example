﻿namespace Domain
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Category Category { get; set; }
    }
}
