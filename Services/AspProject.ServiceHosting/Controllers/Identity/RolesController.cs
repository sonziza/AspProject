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
    [Route(WebAPI.Identity.Roles)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<Role> _roleStore;
        public RolesController(AspProjectDbContext db)
        {
            _roleStore = new RoleStore<Role>(db);
        }
        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllRoles() => await _roleStore.Roles.ToArrayAsync();
    }
}
