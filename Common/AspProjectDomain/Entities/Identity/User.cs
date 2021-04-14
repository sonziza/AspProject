using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectDomain.Entities.Identity
{
    public class User : IdentityUser
    {
        //Здесь удобно использовать технические данные 
        //которые будут использоваться в программе
        //для формирования списка пользователей (и ролей)
        public const string Administrator = "Admin";

        public const string DefaultAdminPassword = "AdPAss";

        public string Description { get; set; }
    }
}
