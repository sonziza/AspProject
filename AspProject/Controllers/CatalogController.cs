using AspProject.Infrastructure.Interfaces;
using AspProject.ViewModel;
using AspProjectDomain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Components
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                   .OrderBy(p => p.Order)
                   .Select(p => new ProductViewModel
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       ImageUrl = p.ImageUrl,
                       Brand = p.Brand?.Name
                   })
            });
        }
        /// <summary>
        /// TODO: избавиться от копипасты функционала в Produtc(s) - с помощью mapping
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);
            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand?.Name
            });
        }
    }
}
