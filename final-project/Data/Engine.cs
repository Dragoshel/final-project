using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FinalProject.Data
{
    public class Engine
    {
        private const string DEV_CONNECTION = "DefaultConnection";

        private const string PROD_CONNECTION = "First_Semester_Project";

        private const string TEST_CONNECTION = "First_Semester_Project_Test";

        private readonly IConfiguration conf;

        public readonly IDbConnection connection;

        public Engine(IConfiguration conf)
        {
            this.conf = conf;

            switch (conf["APP"])
            {
                case "DEV":
                    connection = new SqlConnection(conf.GetConnectionString(DEV_CONNECTION));
                    break;
                case "PROD":
                    var builder = new SqlConnectionStringBuilder(conf.GetConnectionString(PROD_CONNECTION));

                    builder.Password = conf["PASS"];
                    builder.UserID = conf["USER"];
                    builder.InitialCatalog = conf["DB"];
                    builder.DataSource = conf["SERVER"];

                    connection = new SqlConnection(builder.ConnectionString);
                    break;
                case "TEST":
                    connection = new SqlConnection(conf.GetConnectionString(TEST_CONNECTION));
                    break;
                default:
                    connection = new SqlConnection(conf.GetConnectionString(DEV_CONNECTION));
                    break;
            }
        }
    }
}