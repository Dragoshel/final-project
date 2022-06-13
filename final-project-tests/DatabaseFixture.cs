using FinalProject.Data;

namespace FinalProjectTests;

public class DatabaseFixture : IDisposable
{
    private const string CONNECTIONSTRING = "Data Source=; Initial Catalog=; Persist Security Info=False; User ID=; Password=; MultipleActiveResultSets=False; TrustServerCertificate=False; Connection Timeout=30;";

    private const string DATASOURCE = "db";

    private const string INITIALCATALOG = "tempdb";

    private const string USERID = "sa";

    private const string PASSWORD = "Pass1234";

    private const string SQLSCRIPTSDIR = "/Data/SQL";

    private const string FINALPROJECTROOT = "/app/final-project/Data/SQL";

    private static readonly object _lock = new();

    private static bool _databaseInitialized;

    public DatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var engine = CreateEngine())
                {
                    try
                    {
                        // var dirPath = Path.Combine(FINALPROJECTROOT, SQLSCRIPTSDIR);

                        foreach (var filePath in Directory.GetFiles(FINALPROJECTROOT))
                        {
                            using (var reader = File.OpenText(filePath))
                            {
                                engine.SeedDatabase(reader.ReadToEnd());
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("---Could not seed database");
                        Console.WriteLine(ex.Message);
                    }
                }

                _databaseInitialized = true;
            }
        }
    }

    public Engine CreateEngine()
    {
        return new Engine(CONNECTIONSTRING, DATASOURCE, INITIALCATALOG, USERID, PASSWORD);
    }

    public async Task<bool> CheckConnectionAsync()
    {
        using (var con = CreateEngine().MakeConnection())
        {
            try
            {
                await con.OpenAsync();
                return true;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
        }
    }

    public void Dispose() { }
}