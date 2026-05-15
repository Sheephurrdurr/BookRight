

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
            await _context.SeedAsync();
        }

        private async Task SeedClinicsAsync()
        {
            if (_context.Clinics != null) 
            {
                var clinic1 = new Clinic(
                    "Klinik 1",
                    new Address("Gade 1", "By", "1234"),
                    new Domain.ValueObjects     
            }
        }
    }
}
