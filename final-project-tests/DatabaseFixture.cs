using Microsoft.Extensions.Configuration;

using FinalProject.Data;

namespace FinalProjectTests
{
    public class DatabaseFixture : IDisposable
    {
        public readonly IConfiguration conf;

        public readonly Engine engine;

        public DatabaseFixture()
        {
            conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            engine = new Engine(conf);
        }

        public void Dispose()
        {
            engine.connection.Dispose();
        }
    }
}