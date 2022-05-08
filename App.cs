using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject
{
    public class App
    {
        private readonly IConfiguration _conf;

        public App(IConfiguration conf) => _conf = conf;


        public void something()
        {
            var connectionString = _conf["ConnectionStrings:DefaultConnection"];

            Console.WriteLine(connectionString);

        }
    }
}