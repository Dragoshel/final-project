using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalProject.Data
{
    public class Engine
    {
        private readonly IConfiguration conf;

        public IDbConnection connection { get; set; }

        public Engine(IConfiguration conf, IDbConnection connection)
        {
            this.conf = conf;
            this.connection = connection;
        }
    }
}