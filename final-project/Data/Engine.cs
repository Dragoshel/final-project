using System.Data.SqlClient;
using System.Data;

namespace FinalProject.Data;

public class Engine
{
    public readonly IDbConnection connection;

    public Engine(string connectionString, string server, string database, string user, string password)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        builder.DataSource = server;
        builder.InitialCatalog = database;
        builder.UserID = user;
        builder.Password = password;

        connection = new SqlConnection(builder.ConnectionString);
    }
}
