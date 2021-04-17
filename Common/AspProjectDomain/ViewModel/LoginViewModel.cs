using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AspProjectDomain.ViewModel
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        //перенаправляет пользователя на последнюю страницу, которая недоступна неавторизованным пользователям
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
