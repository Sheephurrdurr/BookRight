using BookRight.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Persistence
{
    public class BookRightDbContext : DbContext
    {
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public BookRightDbContext(DbContextOptions<BookRightDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookRightDbContext).Assembly); // Registrer alle modeller der implementer interfacet IEntityTypeConfiguration<T> i DbContext på én gang.
            base.OnModelCreating(modelBuilder);
        }
    }
}
