using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Coordinators;
using PhoneBook.Data;
using PhoneBook.Views;
using PhoneBook.Controllers;
using PhoneBook.Utilities;
using PhoneBook.Services;
using PhoneBook.Mappers;
using PhoneBook.Models;

namespace PhoneBook;
internal class Program
{
    static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

        // Loads settings from JSON file and builds configuration object
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();

        // Register services
        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging
            .ClearProviders()
            .AddDebug();

        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddLogging();
        builder.Services.AddSingleton<MenuHandler>();
        builder.Services.AddSingleton<Validation>();
        builder.Services.AddSingleton<ContactController>();
        builder.Services.AddSingleton<EmailController>();
        builder.Services.AddScoped<DatabaseManager>();
        builder.Services.AddSingleton<ContactMapper>();
        builder.Services.AddSingleton<ListManager>();
        builder.Services.AddSingleton<MailService>();
        builder.Services.AddSingleton<ContactDetailDTO>();
        builder.Services.AddSingleton<CategoryController>();
        builder.Services.AddScoped<MailService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<AppCoordinator>();

        // Build app from services
        var app = builder.Build();

        // Create a scope for DI management
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppCoordinator>>();
        var appCoordinator = scope.ServiceProvider.GetRequiredService<AppCoordinator>();

        appCoordinator.Start();
    }
}

