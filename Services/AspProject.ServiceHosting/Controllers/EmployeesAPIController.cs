﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain.Models;

namespace AspProject.ServiceHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesAPIController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesAPIController(
            IEmployeesData employeesData)
        {
            _EmployeesData = employeesData;
        }


        [HttpGet] //http://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{Id}")]//http://localhost:5001/api/employees/3
        public Employee Get(int id) => _EmployeesData.Get(id);
        
        [HttpGet("employee")]//http://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Олег&Patronymic=Петрович
        public Employee GetByName(string LastName, string FirstName, string Patronymic) => _EmployeesData.GetByName(LastName, FirstName, Patronymic);
       
        [HttpPost]
        public int Add(Employee employee) => _EmployeesData.Add(employee);
       
        [HttpPost("employee")]//http://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Олег&Patronymic=Петрович
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) => _EmployeesData.Add(LastName, FirstName, Patronymic, Age);

        [HttpPut]
        public void Update(Employee employee)
        {
            _EmployeesData.Update(employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id) => _EmployeesData.Delete(id);
    }
}
