using FinalProject.Data;
using FinalProject.Repos;
using FinalProject.Services;

namespace FinalProject.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEngine(this IServiceCollection @this, string connectionString, string server, string database, string user, string password)
    {
        var engine = new Engine(connectionString, server, database, user, password);

        return @this.AddSingleton<Engine>(engine);
    }

    public static IServiceCollection AddRepos(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IBookRepo, BookRepo>()
            .AddScoped<IMemberRepo, MemberRepo>();
    }

    public static IServiceCollection AddServices(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IBookService, BookService>()
            .AddScoped<IMemberService, MemberService>();
    }
}