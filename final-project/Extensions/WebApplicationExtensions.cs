using FinalProject.Data;

namespace FinalProject.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication SeedDatabase(this WebApplication @this, string path)
    {
        try
        {
            var engine = @this.Services.GetRequiredService<Engine>();

            var filePath = Path.Combine(@this.Environment.ContentRootPath, path);

            using (var reader = File.OpenText(filePath))
            {
                engine.SeedDatabase(reader.ReadToEnd());
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