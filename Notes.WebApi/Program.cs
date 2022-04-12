using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Persistence;
using System;

namespace Notes.WebApi
{
    // точка входа в приложение
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // регистрация инжектора зависимостей
            using (var scope = host.Services.CreateScope())
            {
                var serverProvider = scope.ServiceProvider;
                try
                {
                    var context = serverProvider.GetRequiredService<NotesDbContext>();// почему конкретная реализация?
                    DbInitializer.Initialize(context);
                }
                catch (Exception) { }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
