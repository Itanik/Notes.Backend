using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Application;
using Notes.Application.Common.Mapping;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebApi.Middleware;
using System.Reflection;

namespace Notes.WebApi
{
    // ������������ ����������
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ����� �� ����� � ����������
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AsseblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AsseblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            services.AddApplication(); //��������� ������������ �� ������ core (application)
            services.AddPersistence(Configuration); //��������� ������������ �� ������ infrastucture (persistence)
            services.AddControllers();

            services.AddCors(options =>
            {
                // WARNING! ������ cors �������� ����� ����������� ��� ������������
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ����������� � ������ ���������, �.�. ���������� ������ ���� ���������� �� ����� ����������
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // ��� ����� ��������� ��������

            app.UseEndpoints(endpoints =>
            {
                //������������� ������������ � �������� ������������ ��������
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
