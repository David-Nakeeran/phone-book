using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Coordinators;
using PhoneBook.Data;


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
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<AppCoordinator>();

        // Build Service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve AppCoordinator and start app
        var appCoordinator = serviceProvider.GetRequiredService<AppCoordinator>();
        appCoordinator.Start();
    }
}

