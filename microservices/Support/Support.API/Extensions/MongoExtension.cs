using MongoDB.Driver;

namespace Support.API.Extensions;

internal static class MongoExtension
{
    internal static void ConfigureMongo(this IServiceCollection services, ConfigurationManager configuration)
    {
        var mongoConnection = configuration.GetConnectionString("DefaultConnection");
        var mongoClient = new MongoClient(mongoConnection);
        var database = mongoClient.GetDatabase("Chat");

        services.AddSingleton<IMongoDatabase>(database);
    }
}
