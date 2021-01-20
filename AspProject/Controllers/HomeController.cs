using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home controller Action Index");
        }
        public IActionResult Test()
        {
            return Content("Test controller action...");
        }
    }
}
