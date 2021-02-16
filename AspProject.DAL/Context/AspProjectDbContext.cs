using AspProjectDomain.Entities;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspProject.DAL.Context
{
    public class AspProjectDbContext : IdentityDbContext<User, Role, string>
    {
        public AspProjectDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
