using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspProject.DAL.Context;
using Microsoft.EntityFrameworkCore;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using AspProject.Interfaces.Services;
using AspProject.Interfaces.TestAPI;
using AspProject.Services.Data;
using AspProject.Services.Services;
using AspProject.Services.Services.InCookies;
using AspProject.Services.Services.InSQL;
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
            services.AddDbContext<AspProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<AspProjectDBInitializer>();

            //если юзер и роль по умолчанию
            //services.AddIdentity<IdentityUser, IdentityRole>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AspProjectDbContext>()
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

            services.AddTransient<IEmployeesData, EmployeesDataInMemory>();
            //services.AddTransient<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, InSQLProductData>();
            services.AddTransient<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, InSQLOrderService>();
            //в дальнейшем так и будем делать - но пока для наглядности будем разбирать по частям этот паттерн
            //services.AddMvc(); 
            //прописали сервис для работы с контроллерами
            //AddRazorRuntimeCompillation - расширение RuntimeCompillation - для динамического изменения данных
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddScoped<IValuesClientService, ValuesClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AspProjectDBInitializer db)
        {
            db.Initialize();

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
