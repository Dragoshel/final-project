namespace FinalProject
{
    public class MainEntry
    {
        public static async Task Main(string[] args)
        {
            var app = new App();

            await app.Run();
        }
    }
}