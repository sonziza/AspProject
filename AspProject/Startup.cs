using AspProject.Infrastructure.Interfaces;
using AspProject.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspProject.DAL.Context;
using Microsoft.EntityFrameworkCore;
using AspProject.Data;
using AspProject.Infrastructure.Services.InSQL;

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

            services.AddTransient<IEmployeesData, EmployeesDataInMemory>();
            //services.AddTransient<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, InSQLProductData>();


            //в дальнейшем так и будем делать - но пока для наглядности будем разбирать по частям этот паттерн
            //services.AddMvc(); 
            //прописали сервис для работы с контроллерами
            //AddRazorRuntimeCompillation - расширение RuntimeCompillation - для динамического изменения данных
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AspProjectDBInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //для подтягивания файлов на хосте (css файлы, картинки и проч.)
            app.UseStaticFiles();

            app.UseRouting();


            var greetings = Configuration["Greetings"];

            app.UseEndpoints(endpoints =>
            {
                //проецируем маршруты на контроллеры
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
