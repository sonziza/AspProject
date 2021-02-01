﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }
    }
}
