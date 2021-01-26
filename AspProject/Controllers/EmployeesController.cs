using AspProject.Data;
using AspProject.Infrastructure.Interfaces;
using AspProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData employeesData)
        {
            _EmployeesData = employeesData;
        }
        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.Get(id);
                if (employee is not null)
                {
                    return View(employee);
                }
            return NotFound();
        }
    }
}
