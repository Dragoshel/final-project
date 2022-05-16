using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using FinalProject.DI;
using FinalProject.Controllers;
using FinalProject.Models;

namespace FinalProject
{
    public class App
    {
        public readonly IConfiguration conf;

        public readonly IServiceProvider services;

        public App()
        {
            conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<App>()
                .AddEnvironmentVariables()
                // .AddCommandLine(args)
                .Build();

            services = new ServiceCollection()
                .AddLogging(options =>
                    options.ClearProviders().AddConsole().AddDebug()
                )
                .RegisterEngine(conf)
                .RegisterControllers()
                .BuildServiceProvider();
        }

        public async Task Run()
        {
            // Console.Beep();
            var bookController = services.GetRequiredService<IBookController>();
            var logger = services.GetRequiredService<ILogger<IBookController>>();
            var book = new Book
            {
                ISBN = "a2",
                Title = "a",
                Description = "a",
                Edition = "a",
                Subject = "a",
                InStock = true,
                IsLendable = true
            };


            try
            {
                await bookController.CreateAsync(book);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

            }
        }
    }
}