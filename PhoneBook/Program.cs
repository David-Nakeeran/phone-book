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

        // Register services
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddSingleton<AppCoordinator>();

        // Build app from services
        var app = builder.Build();

        // Create a scope for DI management
        using var scope = app.Services.CreateScope();
        var appCoordinator = scope.ServiceProvider.GetRequiredService<AppCoordinator>();
        appCoordinator.Start();
    }
}

