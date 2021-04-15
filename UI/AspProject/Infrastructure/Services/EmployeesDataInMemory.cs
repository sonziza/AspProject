using AspProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.Interfaces.Services;
using AspProjectDomain.Models;

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

        public IEnumerable<Employee> GetAll() => _Employees;

        public Employee Get(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);

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
    }
}
