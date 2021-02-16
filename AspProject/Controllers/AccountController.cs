using AspProject.ViewModel;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register

        public IActionResult Register() => View(new RegisterUserViewModel());

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            //проверка валидации данных
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Регистрация пользователя {0}", Model.UserName);

            //заполним объект данными заполненными юзером
            var user = new User
            {
                UserName = Model.UserName
            };

            //объект user вносим в систему...
            var registration_result = await _UserManager.CreateAsync(user, Model.Password);
            if (registration_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", Model.UserName);

                await _SignInManager.SignInAsync(user, false);
                //после регистрации делаем редирект в Home
                return RedirectToAction("Index", "Home");
            }

            //в случае ошибки собираем инфо ошибок
            _Logger.LogWarning("В процессе регистрации пользователя {0} возникли ошибки {1}",
                Model.UserName,
                string.Join(',', registration_result.Errors.Select(e => e.Description)));

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(Model);
        }

        #endregion
    }
}
