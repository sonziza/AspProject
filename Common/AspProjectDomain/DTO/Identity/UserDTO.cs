using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspProjectDomain.DTO.Identity
{
    public abstract class UserDTO
    {
        public User User { get; set; }

    }
    /// <summary>
    /// когда юзер входит  в систему, система Identity хранит  об этом информацию
    /// 
    /// </summary>
    public class AddLoginDTO  : UserDTO 
    {
        public  UserLoginInfo UserLoginInfo { get; set; }
    }
    /// <summary>
    /// Используется, чтобы хэш пароли не передавать откртыто, а внутри тела
    /// </summary>
    public class PasswordHashDTO : UserDTO
    {
        public  string Hash { get; set; }
    }

    /// <summary>
    /// Процесс блокировки пользователя
    /// </summary>
    public class SetLockOutDTO : UserDTO
    {
        public DateTimeOffset? LockOutEnd { get; set; }
    }

}
