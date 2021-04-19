using System.Collections.Generic;
using AspProjectDomain.Models;

namespace AspProject.Interfaces.Services
{
    public interface IEmployeesData
    {
        Employee GetByName(string LastName, string FirstName, string Patronymic);
        Employee Add(string LastName, string FirstName, string Patronymic, int Age);

        IEnumerable<Employee> Get();
        Employee Get(int id);
        int Add(Employee employee);
        void Update(Employee employee);
        bool Delete(int id);

    }
}
