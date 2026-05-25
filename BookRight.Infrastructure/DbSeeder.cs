using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Aggregates.TherapistAggregate;
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
                "BookRight Vejle Ved Åen",
                new Address("Ågade 10", "Vejle", "7100"),
                new PhoneNumber("12345678"),
                5 // Number of treatment rooms
            );

            var clinic2 = new Clinic(
                "BookRight Vejle Bredballe",
                new Address("Bredballe Center 12", "Vejle", "7120"),
                new PhoneNumber("87654321"),
                4
            );

            var clinic3 = new Clinic(
                "BookRight Egtved",
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

            var clinic1 = _context.Clinics.First(c => c.Name == "BookRight Vejle Ved Åen");
            var clinic2 = _context.Clinics.First(c => c.Name == "BookRight Vejle Bredballe");
            var clinic3 = _context.Clinics.First(c => c.Name == "BookRight Egtved");
            // Klinik Vejle Ved Åen
            var therapist1 = new Therapist(
                new FullName("Hans", "Hansen"),
                new Email("hansen@hans.com"),
                "Massageterapeut",
                new Authorization("Massage", "MAS-1001"),
                clinic1.Id
            );

            var therapist2 = new Therapist(
                new FullName("Lise", "Larsen"),
                new Email("larsen@lise.com"),
                "Fysioterapeut",
                new Authorization("Autoriseret fysioterapeut", "FYS-1001"),
                clinic1.Id
             );

            var therapist3 = new Therapist(
                new FullName("Peter", "Pedersen"),
                new Email("pedersen@peter.com"),
                "Kostvejleder",
                new Authorization("Kostvejledning", "KOS-1001"),
                clinic1.Id
            );

            var therapist4 = new Therapist(
                new FullName("Anna", "Andersen"),
                new Email("andersen@anna.com"),
                "Akupunktør",
                new Authorization("Akupunktur", "AKU-1001"),
                clinic1.Id
            );

            // Klinik Vejle Bredballe

            var therapist5 = new Therapist(
                new FullName("Mette", "Madsen"),
                new Email("madsen@mette.com"),
                "Massageterapeut",
                new Authorization("Massage", "MAS-1002"),
                clinic2.Id
            );

            var therapist6 = new Therapist(
                new FullName("Jens", "Jensen"),
                new Email("jensen@jens.com"),
                "Fysioterapeut",
                new Authorization("Autoriseret fysioterapeut", "FYS-1002"),
                clinic2.Id
            );

            var therapist7 = new Therapist(
                new FullName("Sofie", "Sørensen"),
                new Email("sørensen@sofie.com"),
                "Kostvejleder",
                new Authorization("Kostvejledning", "KOS-1002"),
                clinic2.Id
            );

            var therapist8 = new Therapist(
                new FullName("Lars", "Larsen"),
                new Email("larsen@lars.com"),
                "Akupunktør",
                new Authorization("Akupunktur", "AKU-1003"),
                clinic2.Id
            );

            // Klinik Vejle Egtved

            var therapist9 = new Therapist(
                new FullName("Kirsten", "Kristensen"),
                new Email("kristensen@kirsten.com"),
                "Massageterapeut",
                new Authorization("Massage", "MAS-1003"),
                clinic3.Id
            );

            var therapist10 = new Therapist(
                new FullName("Ole", "Olsen"),
                new Email("olsen@ole.com"),
                "Fysioterapeut",
                new Authorization("Autoriseret fysioterapeut", "FYS-1003"),
                clinic3.Id
            );

            var therapist11 = new Therapist(
                new FullName("Maria", "Møller"),
                new Email("møller@maria.com"),
                "Kostvejleder",
                new Authorization("Kostvejledning", "KOS-1003"),
                clinic3.Id
            );

            var therapist12 = new Therapist(
                new FullName("Niels", "Nielsen"),
                new Email("nielsen@niels.com"),
                "Akupunktør",
                new Authorization("Akupunktur", "AKU-1003"),
                clinic3.Id
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
                new FullName("Bill","Gates"),
                new Email("bill.gates@example.com"),
                new PhoneNumber("87654321"),
                new DateOnly(1990, 1, 1),
                null,
                therapist.Id
            );

            var customer2 = new Customer(
                new FullName("Anna", "Thomsen"),
                new Email("anna.thomsen@example.com"),
                new PhoneNumber("84872234"),
                new DateOnly(1994, 4, 1),
                null,
                therapist.Id
            );

            var customer3 = new Customer(
                new FullName("Mikkel", "Jensen"),
                new Email("mikkel.jensen@example.com"),
                new PhoneNumber("22334455"),
                new DateOnly(1988, 6, 12),
                null,
                therapist.Id
            );

            var customer4 = new Customer(
                new FullName("Sofie", "Larsen"),
                new Email("sofie.larsen@example.com"),
                new PhoneNumber("33445566"),
                new DateOnly(1996, 9, 23),
                null,
                therapist.Id
            );

            var customer5 = new Customer(
                new FullName("Peter", "Nielsen"),
                new Email("peter.nielsen@example.com"),
                new PhoneNumber("44556677"),
                new DateOnly(1979, 11, 5),
                null,
                therapist.Id
            );

            var customer6 = new Customer(
                new FullName("Maria", "Hansen"),
                new Email("maria.hansen@example.com"),
                new PhoneNumber("55667788"),
                new DateOnly(1992, 2, 18),
                null,
                therapist.Id
            );

            var customer7 = new Customer(
                new FullName("Jonas", "Pedersen"),
                new Email("jonas.pedersen@example.com"),
                new PhoneNumber("66778899"),
                new DateOnly(1985, 7, 30),
                null,
                therapist.Id
            );

            var customer8 = new Customer(
                new FullName("Camilla", "Madsen"),
                new Email("camilla.madsen@example.com"),
                new PhoneNumber("77889900"),
                new DateOnly(1998, 12, 14),
                null,
                therapist.Id
            );

            var customer9 = new Customer(
                new FullName("Lars", "Christensen"),
                new Email("lars.christensen@example.com"),
                new PhoneNumber("88990011"),
                new DateOnly(1975, 3, 9),
                null,
                therapist.Id
            );

            var customer10 = new Customer(
                new FullName("Line", "Rasmussen"),
                new Email("line.rasmussen@example.com"),
                new PhoneNumber("99001122"),
                new DateOnly(1991, 8, 21),
                null,
                therapist.Id
            );

            var customer11 = new Customer(
                new FullName("Frederik", "Mortensen"),
                new Email("frederik.mortensen@example.com"),
                new PhoneNumber("11224455"),
                new DateOnly(1983, 5, 17),
                null,
                therapist.Id
            );

            var customer12 = new Customer(
                new FullName("Emma", "Thomsen"),
                new Email("emma.thomsen@example.com"),
                new PhoneNumber("22335566"),
                new DateOnly(1997, 10, 3),
                null,
                therapist.Id
            );

            var customer13 = new Customer(
                new FullName("Nikolaj", "Sørensen"),
                new Email("nikolaj.sorensen@example.com"),
                new PhoneNumber("33446677"),
                new DateOnly(1989, 1, 28),
                null,
                therapist.Id
            );

            var customer14 = new Customer(
                new FullName("Julie", "Andersen"),
                new Email("julie.andersen@example.com"),
                new PhoneNumber("44557788"),
                new DateOnly(1995, 6, 6),
                null,
                therapist.Id
            );

            var customer15 = new Customer(
                new FullName("Martin", "Møller"),
                new Email("martin.moeller@example.com"),
                new PhoneNumber("55668899"),
                new DateOnly(1981, 4, 24),
                null,
                therapist.Id
            );

            var customer16 = new Customer(
                new FullName("Louise", "Kristensen"),
                new Email("louise.kristensen@example.com"),
                new PhoneNumber("66779900"),
                new DateOnly(1993, 9, 12),
                null,
                therapist.Id
            );

            var customer17 = new Customer(
                new FullName("Anders", "Olsen"),
                new Email("anders.olsen@example.com"),
                new PhoneNumber("77880011"),
                new DateOnly(1978, 2, 2),
                null,
                therapist.Id
            );

            var customer18 = new Customer(
                new FullName("Ida", "Johansen"),
                new Email("ida.johansen@example.com"),
                new PhoneNumber("88991122"),
                new DateOnly(2000, 7, 19),
                null,
                therapist.Id
            );

            var customer19 = new Customer(
                new FullName("Thomas", "Bach"),
                new Email("thomas.bach@example.com"),
                new PhoneNumber("99112233"),
                new DateOnly(1986, 11, 27),
                null,
                therapist.Id
            );

            var customer20 = new Customer(
                new FullName("Sara", "Poulsen"),
                new Email("sara.poulsen@example.com"),
                new PhoneNumber("10293847"),
                new DateOnly(1999, 3, 15),
                null,
                therapist.Id
            );



            await _context.Customers.AddRangeAsync(
                customer1, 
                customer2, 
                customer3, 
                customer4, 
                customer5,
                customer6, 
                customer7, 
                customer8, 
                customer9, 
                customer10,
                customer11, 
                customer12, 
                customer13, 
                customer14, 
                customer15,
                customer16, 
                customer17, 
                customer18, 
                customer19, 
                customer20
            );

            await _context.SaveChangesAsync();
        }
    }
}
