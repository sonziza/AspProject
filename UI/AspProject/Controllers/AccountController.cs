using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProjectDomain.ViewModel;

namespace AspProject.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
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

                await _UserManager.AddToRoleAsync(user, Role.Users);
                _Logger.LogInformation("Пользователь {0} наделён ролью {1}", Model.UserName, Role.Users);

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

        #region Login
        //сохраняем в LoginVM последний url для редиректа
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            //Стандартная проверка
            if (!ModelState.IsValid) return View(Model);
            // логинимся с помощью пароля, используя _SignManager
            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else 
                true
#endif
                );

            if (login_result.Succeeded)
            {
                //безопасный редирект авторизованного юзера (домашняя страница, если null)
                return LocalRedirect(Model.ReturnUrl ?? "/");
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль!");

            return View(Model);
        }
        #endregion

        /// <summary>
        /// Выход из логина
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            //передадим в представление ViewBag.ReturnUrl для возврата на предыдущую страницу
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}
