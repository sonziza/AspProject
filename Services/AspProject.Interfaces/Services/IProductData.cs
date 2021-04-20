using System.Collections.Generic;
using AspProjectDomain;
using AspProjectDomain.DTO;
using AspProjectDomain.Entities;

namespace AspProject.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();
        IEnumerable<BrandDTO> GetBrands();
        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);
        ProductDTO GetProductById(int id);
    }
}
