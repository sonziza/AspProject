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
    /// C#9 - ���������� record ��� �������� ����-������������
    /// </summary>
    public record Startup(IConfiguration Configuration)
    {
        /*
        /// <summary>
        /// ��������� �������� ��� ������� � ������������
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ��������� ����� �����������, ����������� ��������� Configuration
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
            //� ���������� ��� � ����� ������ - �� ���� ��� ����������� ����� ��������� �� ������ ���� �������
            //services.AddMvc(); 
            //��������� ������ ��� ������ � �������������
            //AddRazorRuntimeCompillation - ���������� RuntimeCompillation - ��� ������������� ��������� ������
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

            //��� ������������ ������ �� ����� (css �����, �������� � ����.)
            app.UseStaticFiles();

            app.UseRouting();


            var greetings = Configuration["Greetings"];

            app.UseEndpoints(endpoints =>
            {
                //�������� ������������� ������ ���� �� ����� ������ �� ����� �����
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });
                //���������� �������� �� �����������
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
