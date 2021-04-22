using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.DAL.Context;
using AspProject.Interfaces;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspProject.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.Users)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User, Role, AspProjectDbContext> _userStore;

        public UsersController(AspProjectDbContext db)
        {
            _userStore = new UserStore<User, Role, AspProjectDbContext>(db);

        }

        [HttpGet("all")]
        /// <summary>
        /// технический метод. ПОзволяет извлечь всех пользователей. Во взаимодействии с клиентом быть не должен!!!
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsers() => await _userStore.Users.ToArrayAsync();
    }
}
