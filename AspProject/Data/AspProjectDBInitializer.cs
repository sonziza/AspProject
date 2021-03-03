

using AspProject.DAL.Context;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Data
{
    public class AspProjectDBInitializer
    {
        /// <summary>
        /// определяем контекст бд и логгер для наблюдения за процессом инициализации
        /// </summary>
        private readonly AspProjectDbContext _db;
        private readonly ILogger<AspProjectDBInitializer> _Logger;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        public AspProjectDBInitializer(AspProjectDbContext db,
            ILogger<AspProjectDBInitializer> logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _db = db;
            _Logger = logger;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }
        public void Initialize()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация базы данных...");

            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("Выполнение миграций...");
                db.Migrate();
                _Logger.LogInformation("Выполнение миграций выполнено успешно");
            }
            else
                _Logger.LogInformation("База данных находится в актуальной версии ({0:0.0###} c)",
                    timer.Elapsed.TotalSeconds);

            try
            {
                InitializeProducts();
                InitializeIdentityAsync().Wait();

            }
            catch (Exception error)
            {
                _Logger.LogError(error, "Ошибка при выполнении инициализации БД");
                throw;
            }

            _Logger.LogInformation("Инициализация БД выполнена успешно {0}",
                timer.Elapsed.TotalSeconds);
        }
        /// <summary>
        /// Подгрузка тестовых данных из TestData
        /// </summary>
        private void InitializeProducts()
        {
            var timer = Stopwatch.StartNew();

            if (_db.Products.Any()){
                _Logger.LogInformation("Инициализация данных типа {0} не требуется", _db.Products.GetType());
                return;
            }

            _Logger.LogInformation("Инициализация товаров...");

            _Logger.LogInformation("Добавление секций...");
            //Если возникнет какая-нибудь внутренняя ошибка, занесения данных не происходит
            using (_db.Database.BeginTransaction())
            {
                //в контекст подгружаем тестовые данные
                _db.Sections.AddRange(TestData.Sections);
                //Вставляем первичные ключи вручную (включаем эту опцию)
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                //отключаем опцию
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Секции успешно добавлены в БД");

            _Logger.LogInformation("Добавление брендов...");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Бренды успешно добавлены в БД");

            _Logger.LogInformation("Добавление товаров...");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Товары успешно добавлены в БД");

            _Logger.LogInformation("Инициализация товаров выполнена успешно ({0:0.0###})",
                timer.Elapsed.TotalSeconds);
        }
        private async Task InitializeIdentityAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация системы Identity...");

            //Задача на проверку наличия роли ПОЛЬЗОВАТЕЛЬ/АДМИНИСТРАТОР/ и тд
            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                }
            }
            await CheckRole(Role.Administrator);
            await CheckRole(Role.Users);
            
            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Профиль админа не обнаружен. Создаю...");
                var admin = new User
                {
                    UserName = User.Administrator
                };
                //проверка на корректность создания профиля
                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("Учётная запись администратора создана успешно.");
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                    _Logger.LogInformation("Учётная запись администратора наделена ролью {0}", Role.Administrator);
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании учётной записи администратора: {string.Join(",", errors)}");
                }
            }

            _Logger.LogInformation("Инициализация системы Identity прошла успешно...");
        }
    }
}
