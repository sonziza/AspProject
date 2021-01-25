using AspProject.Data;
using AspProject.Infrastructure.Interfaces;
using AspProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Infrastructure.Services
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
        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee)) return employee.Id;
            employee.Id = ++ _MaxId;
            _Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var item = Get(id);
            if (item is null) return false;
            return _Employees.Remove(item);
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee Get(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return;

            var db_item = Get(employee.Id);
            if (db_item is null) return;

            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;
        }
    }
}
