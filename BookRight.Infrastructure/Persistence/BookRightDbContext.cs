using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.Therapist;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Persistence
{
    // DbContext-klassen, som repræsenterer en session med databasen og giver adgang til de forskellige tabeller gennem DbSet<T> egenskaberne.
    public class BookRightDbContext : DbContext
    {
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicOpeningHour> ClinicOpeningHours { get; set; }

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
