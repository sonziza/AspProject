using System;
using System.Collections.Generic;
using System.Linq;
using AspProject.Interfaces.Services;
using AspProject.Services.Data;
using AspProjectDomain.Models;

namespace AspProject.Services.Services
{
    public class EmployeesDataInMemory : IEmployeesData
    {
        private readonly List<Employee> _Employees;
        private int _MaxId;
        public EmployeesDataInMemory()
        {
            _Employees = TestData.Employees;
            _MaxId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 1);
        }


        public IEnumerable<Employee> Get() => _Employees;

        public Employee Get(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);
        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            _Employees.FirstOrDefault(e => e.LastName == LastName && e.FirstName == FirstName && e.Patronymic == Patronymic);

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee)) return employee.Id;
            employee.Id = ++_MaxId;
            _Employees.Add(employee);
            return employee.Id;
        }
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            var employee = new Employee
            {
                LastName = LastName,
                FirstName = FirstName,
                Patronymic = Patronymic,
                Age = Age
            };
            Add(employee);
            return employee;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return;

            var db_set = Get(employee.Id);
            if (db_set is null) return;

            db_set.LastName = employee.LastName;
            db_set.FirstName = employee.FirstName;
            db_set.Patronymic = employee.Patronymic;
            db_set.Age = employee.Age;
        }
        public bool Delete(int id)
        {
            var item = Get(id);
            if (item is null) return false;
            return _Employees.Remove(item);
        }
    }
}
