
using Support.API.Services.App;

namespace Support.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();

            builder.Services.ConfigureHosting(builder.Configuration);


            var app = builder.Build();
            app.ConfigurePipeline();

            app.Run();
        }
    }
}
