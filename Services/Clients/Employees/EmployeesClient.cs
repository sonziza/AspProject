using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspProject.Interfaces;
using Clients.Base;
using Microsoft.Extensions.Configuration;

namespace Clients.Employees
{
    public class EmployeesClient:BaseClient
    {
        private readonly IConfiguration _configuration;

        public EmployeesClient(IConfiguration configuration) : base(configuration, WebAPI.Employees)
        {
        }
    }
}
