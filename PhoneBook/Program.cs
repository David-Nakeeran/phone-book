using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<AppCoordinator>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var appCoordinator = scope.ServiceProvider.GetRequiredService<AppCoordinator>();
        appCoordinator.Start();
    }
}

