using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProject.Interfaces
{
    //Маппинг для контроллеров и клиентов
    public static class WebAPI
    {
        public const string Employees = "api/employees";
        public const string Products = "api/products";
        public const string Orders = "api/orders";
        public class Identity
        {
            public const string Users = "api/users";
            public const string Roles = "api/roles";
        }
    }
}
