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
    // конфигурация приложения
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // какая то магия с рефлексией
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AsseblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AsseblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            services.AddApplication(); //внедрение зависимостей на уровне core (application)
            services.AddPersistence(Configuration); //внедрение зависимостей на уровне infrastucture (persistence)
            services.AddControllers();

            services.AddCors(options =>
            {
                // WARNING! данные cors политики нужно рестриктить для безопасности
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

            // обязательно в начале пайплайна, т.к. исключения должны быть обработаны до всего остального
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // имя ранее созданной политики

            app.UseEndpoints(endpoints =>
            {
                //использование контроллеров в качестве обработчиков запросов
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
