using AspProject.Data;
using AspProject.Infrastructure.Interfaces;
using AspProject.Models;
using AspProject.ViewModel;
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
        #region Edit
        public IActionResult Edit(int id)
        {
            if (id <= 0) return BadRequest();
            var employee = _EmployeesData.Get(id);
            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Post = employee.Post
            });
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
                Post = model.Post
            };
            _EmployeesData.Update(employee);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
