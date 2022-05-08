using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FinalProject
{
    public class MainEntry
    {
        private static void ConfigureServices(IServiceCollection serivces)
        {
            serivces.AddSingleton<App, App>().BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
              IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // .AddEnvironmentVariables()
                // .AddCommandLine(args)
                .Build();

            var services = new ServiceCollection()
                .AddSingleton<App, App>()
                .AddSingleton(Configuration)
                .BuildServiceProvider(true);

            services.GetRequiredService<App>().something();
        }
    }
}