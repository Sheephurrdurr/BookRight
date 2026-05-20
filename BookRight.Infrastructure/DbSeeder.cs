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
                "Klinik Vejle Ved Åen",
                new Address("Ågade 10", "Vejle", "7100"),
                new PhoneNumber("12345678"),
                5 // Number of treatment rooms
            );

            var clinic2 = new Clinic(
                "Klinik Vejle Bredballe",
                new Address("Bredballe Center 12", "Vejle", "7120"),
                new PhoneNumber("87654321"),
                4
            );

            var clinic3 = new Clinic(
                "Klinik Vejle Egtved",
                new Address("Egtvedvej 25", "Egtved", "6040"),
                new PhoneNumber("11223344"),
                4
            );

            await _context.Clinics.AddRangeAsync(clinic1, clinic2, clinic3);
            await _context.SaveChangesAsync();
        }

        private async Task SeedTreatmentTypesAsync()
        {
            if (_context.TreatmentTypes.Any()) return; // Check if there are already treatment types in the database

            var treatmentType1 = new TreatmentType(
                "Sportsmassage 30 min.", 
                30, // Duration in minutes
                1, // Max participants
                new Money(350) // Price for the treatment
            );

            var treatmentType2 = new TreatmentType(
                "Sportsmassage 60 min.",
                60,
                1,
                new Money(699)
            );

            var treatmentType3 = new TreatmentType(
                "Fysioterapi 30 min.",
                30,
                1,
                new Money(395)
            );

            var treatmentType4 = new TreatmentType(
                "Fysioterapi 45 min.",
                45,
                1,
                new Money(589)
            );

            var treatmentType5 = new TreatmentType(
                "Fysioterapi 60 min.",
                60,
                1,
                new Money(745)
            );

            var treatmentType6 = new TreatmentType(
                "Kostvejledning 60 min. førstegangskonsultation",
                60,
                1,
                new Money(799)
            );

            var treatmentType7 = new TreatmentType(
                "Kostvejledning 30 min. opfølgning",
                30,
                1,
                new Money(450)
            );

            var treatmentType8 = new TreatmentType(
                "Akupunktur 45 min.",
                45,
                1,
                new Money(550)
            );

            var treatmentType9 = new TreatmentType(
                "Holdtræning/genoptræning 60 min.",
                60,
                6, // Max participants for group training
                new Money(150)
            );

            // Add the treatment types to the database context
            await _context.TreatmentTypes.AddRangeAsync( 
                treatmentType1, 
                treatmentType2, 
                treatmentType3, 
                treatmentType4, 
                treatmentType5, 
                treatmentType6, 
                treatmentType7, 
                treatmentType8, 
                treatmentType9
                );

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        private async Task SeedTherapistsAsync()
        {
            if (_context.Therapists.Any()) return;

            // Klinik Vejle Ved Åen
            var therapist1 = new Therapist(
                new FullName("Hans", "Hansen"),
                new Email("hansen@hans.com"),
                "Massageterapeut"
            );

            var therapist2 = new Therapist(
                new FullName("Lise", "Larsen"),
                new Email("larsen@lise.com"),
                "Fysioterapeut"
             );

            var therapist3 = new Therapist(
                new FullName("Peter", "Pedersen"),
                new Email("pedersen@peter.com"),
                "Kostvejleder"
            );

            var therapist4 = new Therapist(
                new FullName("Anna", "Andersen"),
                new Email("andersen@anna.com"),
                "Akupunktør"
            );

            // Klinik Vejle Bredballe

            var therapist5 = new Therapist(
                new FullName("Mette", "Madsen"),
                new Email("madsen@mette.com"),
                "Massageterapeut"
            );

            var therapist6 = new Therapist(
                new FullName("Jens", "Jensen"),
                new Email("jensen@jens.com"),
                "Fysioterapeut"
            );

            var therapist7 = new Therapist(
                new FullName("Sofie", "Sørensen"),
                new Email("sørensen@sofie.com"),
                "Kostvejleder"
            );

            var therapist8 = new Therapist(
                new FullName("Lars", "Larsen"),
                new Email("larsen@lars.com"),
                "Akupunktør"
            );

            // Klinik Vejle Egtved

            var therapist9 = new Therapist(
                new FullName("Kirsten", "Kristensen"),
                new Email("kristensen@kirsten.com"),
                "Massageterapeut"
            );

            var therapist10 = new Therapist(
                new FullName("Ole", "Olsen"),
                new Email("olsen@ole.com"),
                "Fysioterapeut"
            );

            var therapist11 = new Therapist(
                new FullName("Maria", "Møller"),
                new Email("møller@maria.com"),
                "Kostvejleder"
            );

            var therapist12 = new Therapist(
                new FullName("Niels", "Nielsen"),
                new Email("nielsen@niels.com"),
                "Akupunktør"
            );

            var sportsmassage30 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 30 min.");
            var fysioterapi60 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 60 min.");
            var kostvejledning60 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 60 min. førstegangskonsultation");
            var akupunktur45 = _context.TreatmentTypes.First(t => t.Name == "Akupunktur 45 min.");
            var holdtraening60 = _context.TreatmentTypes.First(t => t.Name == "Holdtræning/genoptræning 60 min.");

            // Add qualifications for each therapist
            // Massageterapeut
            therapist1.AddQualification(sportsmassage30.Id, 350);
            therapist5.AddQualification(sportsmassage30.Id, 350);
            therapist9.AddQualification(sportsmassage30.Id, 350);

            // Fysioterapeut - her indeholder både fysioterapi og holdtræning,
            // da det er fysioterapeuterne der varetager holdtræningen
            therapist2.AddQualification(fysioterapi60.Id, 745);
            therapist2.AddQualification(holdtraening60.Id, 150);

            therapist6.AddQualification(fysioterapi60.Id, 745);
            therapist6.AddQualification(holdtraening60.Id, 150);

            therapist10.AddQualification(fysioterapi60.Id, 745);
            therapist10.AddQualification(holdtraening60.Id, 150);

            // Kostvejleder
            therapist3.AddQualification(kostvejledning60.Id, 799);
            therapist7.AddQualification(kostvejledning60.Id, 799);
            therapist11.AddQualification(kostvejledning60.Id, 799);

            // Akupunktør
            therapist4.AddQualification(akupunktur45.Id, 550);
            therapist8.AddQualification(akupunktur45.Id, 550);
            therapist12.AddQualification(akupunktur45.Id, 550);

            await _context.Therapists.AddRangeAsync(
                therapist1,
                therapist2,
                therapist3,
                therapist4,
                therapist5,
                therapist6,
                therapist7,
                therapist8,
                therapist9,
                therapist10,
                therapist11,
                therapist12
            );
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
