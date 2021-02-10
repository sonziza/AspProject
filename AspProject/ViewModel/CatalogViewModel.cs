﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.ViewModel
{
    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public int? SectionId { get; init; }

        public int? BrandId { get; set; }
    }
}
