using FinalProject.Data;

namespace FinalProjectTests
{
    public class DatabaseFixture : IDisposable
    {
        private const string CONNECTIONSTRING = "Data Source=; Initial Catalog=; Persist Security Info=False; User ID=; Password=; MultipleActiveResultSets=False; TrustServerCertificate=False; Connection Timeout=30;";

        private const string DATASOURCE = "localhost";

        private const string INITIALCATALOG = "First_Semester_Project";

        private const string USERID = "sa";

        private const string PASSWORD = "Pass1234";

        public readonly Engine engine;

        public DatabaseFixture()
        {
            engine = new Engine(CONNECTIONSTRING, DATASOURCE, INITIALCATALOG, USERID, PASSWORD);
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}