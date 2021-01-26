using AspProject.Infrastructure.Interfaces;
using AspProject.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspProject
{
    /// <summary>
    /// C#9 - используем record для создания авто-конструктора
    /// </summary>
    public record Startup(IConfiguration Configuration)
    {
        /*
        /// <summary>
        /// Добавляем свойство для доступа к конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Добавляем новый конструктор, принимающий интерфейс Configuration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        */

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesData, EmployeesDataInMemory>();
            //в дальнейшем так и будем делать - но пока для наглядности будем разбирать по частям этот паттерн
            //services.AddMvc(); 
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
                app.UseDeveloperExceptionPage();
            }

            //для подтягивания файлов на хосте (css файлы, картинки и проч.)
            app.UseStaticFiles();

            app.UseRouting();


            var greetings = Configuration["Greetings"];

            app.UseEndpoints(endpoints =>
            {
                //муршруты прописываются сверху вниз от самых редких до самых общих
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });
                //проецируем маршруты на контроллеры
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
