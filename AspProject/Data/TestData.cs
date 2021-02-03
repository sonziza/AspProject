using AspProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Data
{
    public class TestData
    {
        //В данном случае моделью является даже не сам Employee, а список сотрудников, EmployeeList
        public static readonly List<Employee> Employees = new()
        {
            new Employee { Id = 1, FirstName = "Иван", LastName = "Сидоров", Patronymic = "Кириллович", Age = 19, Post="Manager" },
            new Employee { Id = 2, FirstName = "Павел", LastName = "Потапкин", Patronymic = "Петрович", Age = 35, Post = "Engineer" },
            new Employee { Id = 3, FirstName = "Антон", LastName = "Иванов", Patronymic = "Дмитриевич", Age = 22, Post = "Engineer" },
            new Employee { Id = 4, FirstName = "Олег", LastName = "Штельнберг", Patronymic = "Леонидович", Age = 43, Post = "Director" },
            new Employee { Id = 5, FirstName = "Иван", LastName = "Хан", Patronymic = "Батрудович", Age = 22, Post = "Manager" },
        };
    }
}
