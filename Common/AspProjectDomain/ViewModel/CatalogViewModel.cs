using System.Collections.Generic;

namespace AspProjectDomain.ViewModel
{
    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public int? SectionId { get; init; }

        public int? BrandId { get; set; }
    }
}
