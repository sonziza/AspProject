using AspProject.Data;
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
        private List<Employee> _Employees;
        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }
        public IActionResult Index()
        {
            return View(_Employees);
        }
        public IActionResult Details(int Id)
        {
            foreach (Employee emp in _Employees)
            {
                if (emp.Id == Id)
                {
                    return View(emp);
                }
            }
            return View("Данные остутствуют");
        }
    }
}
