﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectDomain.Entities.Identity
{
    class Role : IdentityRole
    {
        public const string Administrator = "Administrators";

        public const string Users = "Users";

        public string Description { get; set; }
    }
}
