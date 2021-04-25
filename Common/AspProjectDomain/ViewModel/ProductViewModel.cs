﻿namespace AspProjectDomain.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }
        public string Brand { get; set; }
    }
}
