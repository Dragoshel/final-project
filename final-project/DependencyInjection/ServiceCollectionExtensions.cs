using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FinalProject.Data;
using FinalProject.Controllers;
using FinalProject.Repositories;

namespace FinalProject.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterEngine(this IServiceCollection @this, IConfiguration conf)
        {
            return @this.AddSingleton<Engine>(new Engine(conf));
        }

        public static IServiceCollection RegisterControllers(this IServiceCollection @this)
        {
            return @this
                .AddScoped<IMemberController, MemberController>()
                .AddScoped<IBookController, BookController>()
                .AddScoped<IBookRepo, BookRepo>();
        }
    }
}