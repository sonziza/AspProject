using AspProject.Data;
using AspProjectDomain;
using AspProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces.Services;

namespace AspProject.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;


        public IEnumerable<Section> GetSections() => TestData.Sections;
        public IEnumerable<Product> GetProducts(ProductFilter Filter)
        {
            var query = TestData.Products;
            //ограничения на перечисления (по фильтру)
            //Если что-то есть по фильтру, то производится отбор
            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);

            return query;
        }
        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);
    }
}
