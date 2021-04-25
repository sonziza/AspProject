using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces.TestAPI;

namespace AspProject.Controllers
{
    /// <summary>
    /// КОнтроллер взаимодействия сайта магазина как клиента с Web API
    /// </summary>
    public class WebAPIController : Controller
    {
        private readonly IValuesClientService _ValuesService;

        public WebAPIController(IValuesClientService ValuesService) => _ValuesService = ValuesService;

        public IActionResult Index()
        {
            var values = _ValuesService.Get();
            return View(values);
        }
    }
}
