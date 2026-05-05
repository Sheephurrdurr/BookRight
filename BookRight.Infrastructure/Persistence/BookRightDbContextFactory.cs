using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
            var optionsBuilder = new DbContextOptionsBuilder<BookRightDbContext>();
            optionsBuilder.UseSqlServer("Server=np:\\\\.\\pipe\\LOCALDB#B3A1E4EF\\tsql\\query;Database=BookRight;Trusted_Connection=True;TrustServerCertificate=True;"); // !!VIGTIG!! GEM HARDCODED CONNECTIONSTRING

            return new BookRightDbContext(optionsBuilder.Options);
        }
    }
}
