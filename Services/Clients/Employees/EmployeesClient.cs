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
using Microsoft.Extensions.Logging;

namespace Clients.Employees
{
    public class EmployeesClient:BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger) : base(configuration, WebAPI.Employees)
        {
            _logger = logger;
        }
        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            Get<Employee>($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}");

        public int Add(Employee employee)
        {
            _logger.LogInformation($"Добавление сотрудника {employee}");
            return Post(Address, employee).Content.ReadAsAsync<int>().Result;
        }

        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) =>
            Post($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}", "")
                .Content.ReadAsAsync<Employee>().Result;

        public void Update(Employee employee)
        {
            _logger.LogInformation($"Обновление сотрудника: {employee}");
            Put(Address, employee);
            _logger.LogInformation("Обновление завершено");
        }

        public bool Delete(int id)
        {
            _logger.LogInformation($"Удаление сотрудника с id: {id}");
            using (_logger.BeginScope($"Удаление сотрудника с id: {id}"))
            {
                var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
                _logger.LogInformation("Удаление завершено");
                return result;
            }
        }
    }
}
