using AspProject.DAL.Context;
using AspProject.Infrastructure.Interfaces;
using AspProjectDomain;
using AspProjectDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Infrastructure.Services.InSQL
{
    public record InSQLProductData(AspProjectDbContext db) : IProductData
    {

        public IEnumerable<Section> GetSections() => db.Sections.Include(s => s.Products);

        public IEnumerable<Brand> GetBrands() => db.Brands.Include(b => b.Products);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = db.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);

            return query;
        }
    }
}
