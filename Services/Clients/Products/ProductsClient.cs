using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain;
using AspProjectDomain.DTO;
using Clients.Base;
using Microsoft.Extensions.Configuration;

namespace Clients.Products
{
    public class ProductsClient:BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebAPI.Products) { }

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            Post(Address, Filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<IEnumerable<ProductDTO>>()
                .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");
    }
}
