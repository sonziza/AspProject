using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces.Services;

namespace AspProject.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductController(IProductData ProductData) => _ProductData = ProductData;
        public IActionResult Index()
        {
            return View(_ProductData.GetProducts());
        }
        public IActionResult Edit(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null) return NotFound();
            return View(product); 
        }
            
            //=>
            //_ProductData.GetProductById(id) is { } product
            //    ? View(product)
            //    : NotFound();

        public IActionResult Delete(int id) =>
            _ProductData.GetProductById(id) is { } product
                ? View(product)
                : NotFound();
    }
}
