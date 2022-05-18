using System.Data.SqlClient;
using Dapper;

namespace FinalProject.Data;

public class Engine
{
    private readonly SqlConnectionStringBuilder builder;

    public Engine(string connectionString, string server, string database, string user, string password)
    {
        builder = new SqlConnectionStringBuilder(connectionString);

        builder.DataSource = server;
        builder.InitialCatalog = database;
        builder.UserID = user;
        builder.Password = password;
    }

    public SqlConnection MakeConnection()
    {
        return new SqlConnection(builder.ConnectionString);
    }

    public void SeedDatabase(string sql)
    {
        using (var con = MakeConnection())
        {
            con.Open();
            con.Execute(sql);
        }
    }
}
