using System.Collections.Generic;
using System.Linq;
using AspProject.DAL.Context;
using AspProject.Interfaces.Services;
using AspProjectDomain;
using AspProjectDomain.DTO;
using AspProjectDomain.Entities;
using Microsoft.EntityFrameworkCore;
using WebStore.Services.Mapping;

namespace AspProject.Services.Services.InSQL
{
    public record InSQLProductData(AspProjectDbContext db) : IProductData
    {

        public IEnumerable<SectionDTO> GetSections() => db.Sections.Include(s => s.Products).ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => db.Brands.Include(b => b.Products).ToDTO();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            //подключаем дополнительные таблицы к Продуктам
            IQueryable<Product> query = db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);
            if (Filter?.Ids?.Length > 0)
                //если в фильтре указаны идентификаторы товаров (корзины)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }

            return query.AsEnumerable().ToDTO();
        }

        public ProductDTO GetProductById(int id) =>
            db.Products
              .Include(product => product.Brand)
              .Include(product => product.Section)
              .FirstOrDefault(product => product.Id == id).ToDTO();        
    }
}
