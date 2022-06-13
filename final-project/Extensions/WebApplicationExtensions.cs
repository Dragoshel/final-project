using FinalProject.Data;

namespace FinalProject.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication SeedDatabaseFromFiles(this WebApplication @this, string path)
    {
        try
        {
            var engine = @this.Services.GetRequiredService<Engine>();

            var dirPath = Path.Combine(@this.Environment.ContentRootPath, path);

            Console.WriteLine(dirPath);

            foreach (var filePath in Directory.GetFiles(dirPath))
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

        return @this;
    }
}