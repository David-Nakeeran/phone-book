using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace PhoneBook;
internal class Program
{
    static void Main(string[] args)
    {
        // Loads settings from JSON file and builds configuration object
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Create service collection
        var services = new ServiceCollection();

        // Register the services
        services.


    }
}

