using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain;
using AspProjectDomain.DTO;

namespace AspProject.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsAPIController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsAPIController( IProductData productData) => _productData = productData;
        
        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            return _productData.GetProducts(Filter);
        }
        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }

        [HttpGet("sections/{id}")]
        public SectionDTO GetSectionById(int id)
        {
            return _productData.GetSectionById(id);
        }

        [HttpGet("brands/{id}")]
        public BrandDTO GetBrandById(int id)
        {
            return _productData.GetBrandById(id);
        }
    }
}
