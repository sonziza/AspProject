using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using AspProject.Interfaces.Services;
using AspProject.Interfaces.TestAPI;
using AspProject.Services.Services.InCookies;
using AspProject.Services.Services.InSQL;
using Clients.Employees;
using Clients.Identity;
using Clients.Orders;
using Clients.Products;
using Clients.Values;

namespace AspProject
{
    /// <summary>
    /// C#9 - используем record для создания авто-конструктора
    /// Подгружаем конфиг данные appsetttings.json
    /// </summary>
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                //подключаем коллекцию интерфейсов для системы Identity
                .AddIdentityWebAPIClients()
                .AddDefaultTokenProviders();


            //конфигурация системы Identity
            // - требование к паролю
            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false; //будет ли использоваться почта логином
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            //конфигурация системы Cookies
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "AspProject.ru";
                opt.Cookie.HttpOnly = true; //не передавать в куки ftp tcp и проч протоколы
                opt.ExpireTimeSpan = TimeSpan.FromDays(10); //через 10 дней куки "протухнут"

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                //этот параметро заставляет сделать новый id сеанса для разделения пользователей анонимов и залогиненых
                opt.SlidingExpiration = true;
            });

            //Конфигурация прочих сервисов
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, ProductsClient>();
            services.AddTransient<IOrderService, OrdersClient>();
            services.AddScoped<IValuesClientService, ValuesClient>();
            services.AddTransient<ICartService, InCookiesCartService>();
            //прописали сервис для работы с контроллерами
            //AddRazorRuntimeCompillation - расширение RuntimeCompillation - для динамического изменения данных
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    //включаем отладочную страницу в браузере
            }

            //для подтягивания файлов на хосте (css файлы, картинки и проч.)
            app.UseStaticFiles();

            //система маршрутизации
            app.UseRouting();

            //требования аутен-ции и автор-ции
            //извлекает инфо о пользователе
            app.UseAuthentication();
            //проверяется доступ по маршруту
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //маршрутизация области (Админка, например)
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );
                //проецируем маршруты на контроллеры
                //Основной маршрут должен быть самым последним
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
