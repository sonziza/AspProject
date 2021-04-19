using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces.Services;
using AspProjectDomain.Models;
using AspProjectDomain.ViewModel;

namespace AspProject.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData employeesData)
        {
            _EmployeesData = employeesData;
        }
        public IActionResult Index()
        {
            return View(_EmployeesData.Get());
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
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Create()
        {
            return View("Edit", new EmployeeViewModel());
        }
        #region Edit
        [Authorize(Roles = Role.Administrator)]
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
        [Authorize(Roles = Role.Administrator)]
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

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }
        #endregion
        #region delete
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
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
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
