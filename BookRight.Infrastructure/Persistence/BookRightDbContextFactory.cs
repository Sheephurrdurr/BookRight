using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookRight.Infrastructure.Persistence
{
    // DbContextFactory is a "place" from which we run EF Core database migrations. 
    // Previously we've been doing it in ConsoleUI's Program.cs, but now we're doing it in a factory file, like this one.
    // Before we used EnsureCreated(), which merely creates the database when the method is called.
    // With this 'factory approach' we can run database migrations from in here, which will then update the database with changes. 
    public class BookRightDbContextFactory : IDesignTimeDbContextFactory<BookRightDbContext>
    {
        public BookRightDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "BookRight.BlazorUI")) // go "up" a folder and find BlazorUI there to use connectionstring
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional : true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BookRightDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new BookRightDbContext(optionsBuilder.Options);
        }
    }
}
