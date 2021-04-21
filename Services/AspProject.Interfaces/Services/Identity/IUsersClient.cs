using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspProject.Interfaces.Services.Identity
{
    public interface IUsersClient:
        IUserStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserClaimStore<User>
    {
    }
}
