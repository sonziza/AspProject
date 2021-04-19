using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain.Models;
using Clients.Base;
using Microsoft.Extensions.Configuration;

namespace Clients.Employees
{
    public class EmployeesClient:BaseClient, IEmployeesData
    {

        public EmployeesClient(IConfiguration configuration) : base(configuration, WebAPI.Employees)
        {
        }
        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            Get<Employee>($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}");

        public int Add(Employee employee) => Post(Address, employee).Content.ReadAsAsync<int>().Result;

        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) =>
            Post($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}", "")
                .Content.ReadAsAsync<Employee>().Result;

        public void Update(Employee employee) => Put(Address, employee);

        public bool Delete(int id)
        {
            var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
            return result;
        }
    }
}
