using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.ValueObjects;
using BookRight.Infrastructure.Persistence;

namespace BookRight.Infrastructure
{
    public class DbSeeder
    {
        private readonly BookRightDbContext _context;

        public DbSeeder(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedClinicsAsync();
        }

        private async Task SeedClinicsAsync()
        {
            if (_context.Clinics.Any()) return; 
            
            var clinic1 = new Clinic(
                "Klinik 1",
                new Address("Gade 1", "By", "1234"),
                new PhoneNumber("12345678"),
                5
            );

            await _context.Clinics.AddAsync(clinic1);
            await _context.SaveChangesAsync();

            
        }
    }
}
