using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

using FinalProject.Data;
using FinalProject.Controllers;
using FinalProject.Repositories;

namespace FinalProject
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterEngine(this IServiceCollection @this, IConfiguration conf)
        {
            // var builder = new SqlConnectionStringBuilder(conf.GetConnectionString("First_Semester_Project"));

            // builder.Password = conf["Password"];
            // builder.UserID = conf["User"];
            // builder.InitialCatalog = conf["DbName"];
            // builder.DataSource = conf["Server"];

            // var sqlConnection = new SqlConnection(builder.ConnectionString);
            var sqlConnection = new SqlConnection(conf.GetConnectionString("DefaultConnection"));
            return @this.AddSingleton<Engine>(new Engine(conf, sqlConnection));
        }

        public static IServiceCollection RegisterControllers(this IServiceCollection @this)
        {
            return @this
                .AddScoped<IMemberController, MemberController>()
                .AddScoped<IBookController, BookController>()
                .AddScoped<IBookRepo, BookRepo>();
        }
    }

    public class App
    {
        public readonly IConfiguration conf;

        public readonly IServiceProvider services;

        public App()
        {
            conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<App>()
                // .AddCommandLine(args)
                .Build();

            services = new ServiceCollection()
                .AddSingleton(conf)
                .RegisterEngine(conf)
                .RegisterControllers()
                .BuildServiceProvider();
        }

        public async Task Run()
        {
            // var bookController = services.GetRequiredService<IBookController>();
        }
    }
}