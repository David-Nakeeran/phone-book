using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data;

class ApplicationDbContext : DbContext
{
    public DbSet<ContactDetail> ContactDetails { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    // ApplicationDbContext is used by Entity Framework Core to interact with the database.
    // A DbSet property representing the ContactDetails table in the database.
    // Entity Framework will use this property to query and save instances of ContactDetail.
    // The constructor takes DbContextOptions, which provide configuration settings for EF Core,
    // such as the database provider and connection details. These options are passed to the base DbContext class.
    // Passes the options to the base DbContext constructor so EF Core can use the configuration.

}