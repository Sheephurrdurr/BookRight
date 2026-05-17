using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Aggregates.Therapist;
using BookRight.Domain.Aggregates.Customer;
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
            await SeedTreatmentTypesAsync();
            await SeedTherapistsAsync();
            await SeedCustomersAsync();
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

        private async Task SeedTreatmentTypesAsync()
        {
            if (_context.TreatmentTypes.Any()) return;

            var treatmentType1 = new TreatmentType(
                "Massage",
                30,
                500,
                new Money(500)
            );

            await _context.TreatmentTypes.AddAsync(treatmentType1);
            await _context.SaveChangesAsync();
        }

        private async Task SeedTherapistsAsync()
        {
            if (_context.Therapists.Any()) return;

            var therapist1 = new Therapist(
                new FullName("Hans", "Hansen"),
                new Email("hansen@hans.com"),
                "Massage"
            );

            var treatmentType = _context.TreatmentTypes.First();

            therapist1.AddQualification(treatmentType.Id, 500);

            await _context.Therapists.AddAsync(therapist1);
            await _context.SaveChangesAsync();
        }

        public async Task SeedCustomersAsync()
        {
            if (_context.Customers.Any()) return;

            var therapist = _context.Therapists.First();

            var customer1 = new Customer(
                new FullName("Customer", "Bill"),
                new Email("customer@bill.com"),
                new PhoneNumber("87654321"),
                new DateOnly(1990, 1, 1),
                null,
                therapist.Id
            );

            await _context.Customers.AddAsync(customer1);
            await _context.SaveChangesAsync();
        }
    }
}
