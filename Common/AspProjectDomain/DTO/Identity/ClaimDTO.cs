using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectDomain.DTO.Identity
{
    /// <summary>
    /// Утверждение, хранящее инфо о данных пользователя в момент авторизации
    /// </summary>
    public class ClaimDTO: UserDTO
    {
        public  IEnumerable<Claim> Claims { get; set; }
    }
    public class AddClaimDTO : ClaimDTO { }

    public class RemoveClaimDTO : ClaimDTO { }

    public class ReplaceClaimDTO : UserDTO
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}
