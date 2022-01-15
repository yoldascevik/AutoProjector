using Microsoft.Extensions.Configuration;

namespace MongoDBAutoProject.Test.Helpers;

public class ConfigurationHelper
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        return config;
    }
}