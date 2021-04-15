using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        /// <summary>
        /// логика представления шапки сайта в зависимости от авторизации клиента
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke() => User.Identity?.IsAuthenticated == true
            ? View("UserInfo")
            : View();

    }
}
