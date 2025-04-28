using BuildingBlocks.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace Support.API.Services.App
{
    public class AppConfiguration(IConfiguration configuration) : IAppConfiguration
    {
        public string GetValue(string key)
        {
            return configuration[key]!;
        }
    }
}
