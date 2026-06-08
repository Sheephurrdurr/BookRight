using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Enums;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            await SeedBookingsAsync();

            await SeedGeneratedCustomersAsync();
            await SeedGeneratedBookingsAsync();
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
                new Money(350),// Price for the treatment
                true, // Can be combined with other treatments?
                "Massage"
            );

            var treatmentType2 = new TreatmentType(
                "Sportsmassage 60 min.",
                60,
                1,
                new Money(699),
                true,
                "Autoriseret fysioterapeut"
            );

            var treatmentType3 = new TreatmentType(
                "Fysioterapi 30 min.",
                30,
                1,
                new Money(395),
                true,
                "Autoriseret fysioterapeut"
            );

            var treatmentType4 = new TreatmentType(
                "Fysioterapi 45 min.",
                45,
                1,
                new Money(589),
                true,
                "Autoriseret fysioterapeut"
            );

            var treatmentType5 = new TreatmentType(
                "Fysioterapi 60 min.",
                60,
                1,
                new Money(745),
                true,
                "Autoriseret fysioterapeut"
            );

            var treatmentType6 = new TreatmentType(
                "Kostvejledning 60 min. førstegangskonsultation",
                60,
                1,
                new Money(799),
                false,
                "Kostvejledning"
            );

            var treatmentType7 = new TreatmentType(
                "Kostvejledning 30 min. opfølgning",
                30,
                1,
                new Money(450),
                false,
                "Kostvejledning"
            );

            var treatmentType8 = new TreatmentType(
                "Akupunktur 45 min.",
                45,
                1,
                new Money(550),
                false,
                "Akupunktur"
            );

            var treatmentType9 = new TreatmentType(
                "Holdtræning/genoptræning 60 min.",
                60,
                6, // Max participants for group training
                new Money(150),
                false,
                "Autoriseret fysioterapeut"
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
            var sportmassage60 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 60 min.");

            var fysioterapi30 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 30 min.");
            var fysioterapi45 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 45 min.");
            var fysioterapi60 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 60 min.");

            var kostvejledning30 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 30 min. opfølgning");
            var kostvejledning60 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 60 min. førstegangskonsultation");

            var akupunktur45 = _context.TreatmentTypes.First(t => t.Name == "Akupunktur 45 min.");

            var holdtraening60 = _context.TreatmentTypes.First(t => t.Name == "Holdtræning/genoptræning 60 min.");

            // Add qualifications for each therapist
            // Massageterapeut
            therapist1.AddQualification(sportsmassage30.Id, 350);
            therapist1.AddQualification(sportmassage60.Id, 699);

            therapist5.AddQualification(sportsmassage30.Id, 350);
            therapist5.AddQualification(sportmassage60.Id, 699); 

            therapist9.AddQualification(sportsmassage30.Id, 350);


            // Fysioterapeut - her indeholder både fysioterapi og holdtræning,
            // da det er fysioterapeuterne der varetager holdtræningen
            therapist2.AddQualification(fysioterapi30.Id, 395);
            therapist2.AddQualification(fysioterapi45.Id, 589);
            therapist2.AddQualification(fysioterapi60.Id, 745);

            therapist2.AddQualification(holdtraening60.Id, 150);

            therapist6.AddQualification(fysioterapi30.Id, 395);
            therapist6.AddQualification(fysioterapi45.Id, 589);
            therapist6.AddQualification(fysioterapi60.Id, 745);
            therapist6.AddQualification(holdtraening60.Id, 150);

            therapist10.AddQualification(fysioterapi30.Id, 395);
            therapist10.AddQualification(fysioterapi45.Id, 589);
            therapist10.AddQualification(fysioterapi60.Id, 745);
            therapist10.AddQualification(holdtraening60.Id, 150);


            // Kostvejleder
            therapist3.AddQualification(kostvejledning30.Id, 450);
            therapist3.AddQualification(kostvejledning60.Id, 799);

            therapist7.AddQualification(kostvejledning30.Id, 450);
            therapist7.AddQualification(kostvejledning60.Id, 799);

            therapist11.AddQualification(kostvejledning30.Id, 450);
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
                new FullName("Bill", "Gates"),
                new Email("bill.gates@example.com"),
                new Address("Vesterbrogade 14", "Vejle", "7100"),
                new PhoneNumber("87654321"),
                new DateOnly(1990, 1, 1),
                null,
                therapist.Id
            );

            var customer2 = new Customer(
                new FullName("Anna", "Thomsen"),
                new Email("anna.thomsen@example.com"),
                new Address("Nørretorv 3", "Vejle", "7100"),
                new PhoneNumber("84872234"),
                new DateOnly(1994, 4, 1),
                null,
                therapist.Id
            );

            var customer3 = new Customer(
                new FullName("Mikkel", "Jensen"),
                new Email("mikkel.jensen@example.com"),
                new Address("Søndergade 22", "Vejle", "7100"),
                new PhoneNumber("22334455"),
                new DateOnly(1988, 6, 12),
                null,
                therapist.Id
            );

            var customer4 = new Customer(
                new FullName("Sofie", "Larsen"),
                new Email("sofie.larsen@example.com"),
                new Address("Horsensvej 8", "Vejle", "7100"),
                new PhoneNumber("33445566"),
                new DateOnly(1996, 9, 23),
                null,
                therapist.Id
            );

            var customer5 = new Customer(
                new FullName("Peter", "Nielsen"),
                new Email("peter.nielsen@example.com"),
                new Address("Kirketorvet 5", "Vejle", "7100"),
                new PhoneNumber("44556677"),
                new DateOnly(1979, 11, 5),
                null,
                therapist.Id
            );

            var customer6 = new Customer(
                new FullName("Maria", "Hansen"),
                new Email("maria.hansen@example.com"),
                new Address("Strandvejen 12", "Vejle", "7100"),
                new PhoneNumber("55667788"),
                new DateOnly(1992, 2, 18),
                null,
                therapist.Id
            );

            var customer7 = new Customer(
                new FullName("Jonas", "Pedersen"),
                new Email("jonas.pedersen@example.com"),
                new Address("Bredgade 7", "Kolding", "6000"),
                new PhoneNumber("66778899"),
                new DateOnly(1985, 7, 30),
                null,
                therapist.Id
            );

            var customer8 = new Customer(
                new FullName("Camilla", "Madsen"),
                new Email("camilla.madsen@example.com"),
                new Address("Haderslevvej 19", "Kolding", "6000"),
                new PhoneNumber("77889900"),
                new DateOnly(1998, 12, 14),
                null,
                therapist.Id
            );

            var customer9 = new Customer(
                new FullName("Lars", "Christensen"),
                new Email("lars.christensen@example.com"),
                new Address("Akseltorv 2", "Kolding", "6000"),
                new PhoneNumber("88990011"),
                new DateOnly(1975, 3, 9),
                null,
                therapist.Id
            );

            var customer10 = new Customer(
                new FullName("Line", "Rasmussen"),
                new Email("line.rasmussen@example.com"),
                new Address("Rendebanen 11", "Kolding", "6000"),
                new PhoneNumber("99001122"),
                new DateOnly(1991, 8, 21),
                null,
                therapist.Id
            );

            var customer11 = new Customer(
                new FullName("Frederik", "Mortensen"),
                new Email("frederik.mortensen@example.com"),
                new Address("Torvet 1", "Horsens", "8700"),
                new PhoneNumber("11224455"),
                new DateOnly(1983, 5, 17),
                null,
                therapist.Id
            );

            var customer12 = new Customer(
                new FullName("Emma", "Thomsen"),
                new Email("emma.thomsen@example.com"),
                new Address("Søndergade 4", "Horsens", "8700"),
                new PhoneNumber("22335566"),
                new DateOnly(1997, 10, 3),
                null,
                therapist.Id
            );

            var customer13 = new Customer(
                new FullName("Nikolaj", "Sørensen"),
                new Email("nikolaj.sorensen@example.com"),
                new Address("Fussingsvej 6", "Horsens", "8700"),
                new PhoneNumber("33446677"),
                new DateOnly(1989, 1, 28),
                null,
                therapist.Id
            );

            var customer14 = new Customer(
                new FullName("Julie", "Andersen"),
                new Email("julie.andersen@example.com"),
                new Address("Nørrebrogade 33", "Horsens", "8700"),
                new PhoneNumber("44557788"),
                new DateOnly(1995, 6, 6),
                null,
                therapist.Id
            );

            var customer15 = new Customer(
                new FullName("Martin", "Møller"),
                new Email("martin.moeller@example.com"),
                new Address("Fredericiavej 41", "Vejle", "7100"),
                new PhoneNumber("55668899"),
                new DateOnly(1981, 4, 24),
                null,
                therapist.Id
            );

            var customer16 = new Customer(
                new FullName("Louise", "Kristensen"),
                new Email("louise.kristensen@example.com"),
                new Address("Boulevarden 16", "Vejle", "7100"),
                new PhoneNumber("66779900"),
                new DateOnly(1993, 9, 12),
                null,
                therapist.Id
            );

            var customer17 = new Customer(
                new FullName("Anders", "Olsen"),
                new Email("anders.olsen@example.com"),
                new Address("Give Landevej 55", "Give", "7323"),
                new PhoneNumber("77880011"),
                new DateOnly(1978, 2, 2),
                null,
                therapist.Id
            );

            var customer18 = new Customer(
                new FullName("Ida", "Johansen"),
                new Email("ida.johansen@example.com"),
                new Address("Vestergade 9", "Juelsminde", "7130"),
                new PhoneNumber("88991122"),
                new DateOnly(2000, 7, 19),
                null,
                therapist.Id
            );

            var customer19 = new Customer(
                new FullName("Thomas", "Bach"),
                new Email("thomas.bach@example.com"),
                new Address("Grundet Ringvej 28", "Vejle", "7100"),
                new PhoneNumber("99112233"),
                new DateOnly(1986, 11, 27),
                null,
                therapist.Id
            );

            var customer20 = new Customer(
                new FullName("Sara", "Poulsen"),
                new Email("sara.poulsen@example.com"),
                new Address("Munkebjergvej 13", "Vejle", "7100"),
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
        private async Task SeedGeneratedCustomersAsync()
        {
            if (_context.Customers.Count() >= 300)
                return;
            var existingCustomerCount = _context.Customers.Count();
            var customersToCreate = 300 - existingCustomerCount;

            if (customersToCreate <= 0)
                return;

            var therapists = _context.Therapists.ToList();
            var random = new Random();

            var firstNames = new[]
             {
                "Mads","Emma","Noah","Freja","William","Clara","Oscar","Ida","Carl","Alma",
                "Lucas","Sofia","Malthe","Ella","Oliver","Anna","Emil","Liva","Victor","Mille",
                "Alexander","Laura","Benjamin","Josefine","Felix","Mathilde","Sebastian","Sarah",
                "Magnus","Julie","Anton","Karla","Theodor","Marie","Valdemar","Nanna","August",
                "Agnes","Elias","Johanne","Aksel","Lea","Frederik","Cecilie","Christian","Line",
                "Andreas","Camilla","Simon","Maja"
             };

            var lastNames = new[]
            {
                "Jensen","Nielsen","Hansen","Pedersen","Andersen","Christensen","Larsen",
                "Sørensen","Rasmussen","Madsen","Kristensen","Olsen","Thomsen","Poulsen",
                "Knudsen","Mortensen","Lund","Holm","Friis","Mikkelsen","Jeppesen","Bach",
                "Lauridsen","Svendsen","Vestergaard","Dam","Møller","Hjort","Bruun","Bonde",
                "Bjerre","Gregersen","Bøgh","Overgaard","Winther","Kjær","Toft","Schmidt",
                "Skov","Leth"
            };

            var cities = new[]
            {
                ("Vejle", "7100"),
                ("Bredballe", "7120"),
                ("Egtved", "6040"),
                ("Kolding", "6000"),
                ("Horsens", "8700")
             };

            var generatedCustomers = new List<Customer>();
            for (int i = 1; i <= customersToCreate; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];

                var cityInfo = cities[random.Next(cities.Length)];

                var therapist = therapists[random.Next(therapists.Count)];

                var customer = new Customer(
                    new FullName(firstName, lastName),
                    new Email($"{firstName.ToLower()}.{lastName.ToLower()}{i}@example.com"),
                    new Address($"Testvej {random.Next(1, 100)}", cityInfo.Item1, cityInfo.Item2),
                    new PhoneNumber($"4{random.Next(1000000, 9999999)}"),
                    new DateOnly(
                        random.Next(1950, 2010),
                        random.Next(1, 13),
                        random.Next(1, 28)),
                    null,
                    therapist.Id
                );

                generatedCustomers.Add(customer);

                await _context.Customers.AddRangeAsync(generatedCustomers);
                await _context.SaveChangesAsync();

                var allCustomers = _context.Customers.ToList();

                var noLoyaltyCustomers = allCustomers.Take(75).ToList();

                var bronzeCustomers = allCustomers
                    .Skip(75)
                    .Take(75)
                    .ToList();

                var silverCustomers = allCustomers
                    .Skip(150)
                    .Take(75)
                    .ToList();

                var goldCustomers = allCustomers
                    .Skip(225)
                    .Take(75)
                    .ToList();
            }
        }
            

        

        // This method creates a variety of bookings with different statuses, treatment types, therapists,
        // and customers to provide a rich dataset for testing and development purposes.
        private async Task SeedBookingsAsync()
        {
            if (_context.Bookings.Any()) return;

            var customers = _context.Customers.ToList();

            var therapists = _context.Therapists
                .Include(t => t.Qualifications)
                .ToList();

            var clinic1 = _context.Clinics.First(c => c.Name == "BookRight Vejle Ved Åen");
            var clinic2 = _context.Clinics.First(c => c.Name == "BookRight Vejle Bredballe");
            var clinic3 = _context.Clinics.First(c => c.Name == "BookRight Egtved");

            // TreatmentTypes
            var massage30 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 30 min.");
            var massage60 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 60 min.");
            var fys30 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 30 min.");
            var fys45 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 45 min.");
            var fys60 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 60 min.");
            var kost60 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 60 min. førstegangskonsultation");
            var kost30 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 30 min. opfølgning");
            var aku45 = _context.TreatmentTypes.First(t => t.Name == "Akupunktur 45 min.");
            var hold60 = _context.TreatmentTypes.First(t => t.Name == "Holdtræning/genoptræning 60 min.");

            // Therapists
            var therapistMassage1 = therapists.First(t => t.Name.FirstName == "Hans" && t.Name.LastName == "Hansen");
            var therapistFys1 = therapists.First(t => t.Name.FirstName == "Lise" && t.Name.LastName == "Larsen");
            var therapistKost1 = therapists.First(t => t.Name.FirstName == "Peter" && t.Name.LastName == "Pedersen");
            var therapistAku1 = therapists.First(t => t.Name.FirstName == "Anna" && t.Name.LastName == "Andersen");

            var therapistMassage2 = therapists.First(t => t.Name.FirstName == "Mette" && t.Name.LastName == "Madsen");
            var therapistFys2 = therapists.First(t => t.Name.FirstName == "Jens" && t.Name.LastName == "Jensen");
            var therapistKost2 = therapists.First(t => t.Name.FirstName == "Sofie" && t.Name.LastName == "Sørensen");
            var therapistAku2 = therapists.First(t => t.Name.FirstName == "Lars" && t.Name.LastName == "Larsen");

            var therapistMassage3 = therapists.First(t => t.Name.FirstName == "Kirsten" && t.Name.LastName == "Kristensen");
            var therapistFys3 = therapists.First(t => t.Name.FirstName == "Ole" && t.Name.LastName == "Olsen");
            var therapistKost3 = therapists.First(t => t.Name.FirstName == "Maria" && t.Name.LastName == "Møller");
            var therapistAku3 = therapists.First(t => t.Name.FirstName == "Niels" && t.Name.LastName == "Nielsen");

            var june = new DateTime(2026, 6, 1);

            var bookings = new List<Booking>();

            Booking CreateBooking(
                Customer customer,
                Therapist therapist,
                Clinic clinic,
                DateTime startTime,
                int durationMinutes,
                TreatmentType treatmentType,
                decimal price,
                decimal discountPercent = 0,
                DiscountType discountType = DiscountType.None,
                BookingStatus status = BookingStatus.Confirmed)
            {
                var therapistTreatmentTypeId = therapist.Qualifications
                    .First(q => q.TreatmentTypeId == treatmentType.Id)
                    .Id;

                var booking = new Booking(
                    Guid.NewGuid(),
                    customer.Id,
                    therapist.Id,
                    clinic.Id,
                    new TimeSlot(startTime, startTime.AddMinutes(durationMinutes))
                );

                booking.AddLine(new BookingLine(
                    therapistTreatmentTypeId,
                    new Money(price),
                    discountPercent,
                    discountType
                ));

                if (status == BookingStatus.Completed)
                    booking.Complete();

                if (status == BookingStatus.NoShow)
                    booking.MarkAsNoShow();

                if (status == BookingStatus.Cancelled)
                    booking.Cancel();

                if (status == BookingStatus.Arrived)
                    booking.MarkAsArrived();

                return booking;
            }


            // Klinik 1 - BookRight Vejle Ved Åen
            bookings.Add(CreateBooking(customers[0], therapistMassage1, clinic1, june.AddDays(0).AddHours(9), 30, massage30, 350, 0, DiscountType.None, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[1], therapistFys1, clinic1, june.AddDays(0).AddHours(10), 60, fys60, 745, 10, DiscountType.Loyalty, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[2], therapistKost1, clinic1, june.AddDays(1).AddHours(11), 30, kost30, 450, 0, DiscountType.None, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[3], therapistAku1, clinic1, june.AddDays(1).AddHours(13), 45, aku45, 550, 25, DiscountType.Birthday, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[4], therapistMassage1, clinic1, june.AddDays(2).AddHours(14), 60, massage60, 699, 0, DiscountType.None, BookingStatus.NoShow));
            bookings.Add(CreateBooking(customers[5], therapistFys1, clinic1, june.AddDays(3).AddHours(9), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[6], therapistMassage1, clinic1, june.AddDays(4).AddHours(10), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[7], therapistKost1, clinic1, june.AddDays(7).AddHours(12), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[8], therapistAku1, clinic1, june.AddDays(8).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[9], therapistFys1, clinic1, june.AddDays(9).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[10], therapistMassage1, clinic1, june.AddDays(10).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[11], therapistFys1, clinic1, june.AddDays(11).AddHours(10), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[12], therapistKost1, clinic1, june.AddDays(14).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[13], therapistAku1, clinic1, june.AddDays(15).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[14], therapistFys1, clinic1, june.AddDays(16).AddHours(14), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[15], therapistMassage1, clinic1, june.AddDays(17).AddHours(9), 60, massage60, 699));
            bookings.Add(CreateBooking(customers[16], therapistFys1, clinic1, june.AddDays(17).AddHours(11), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[17], therapistKost1, clinic1, june.AddDays(18).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[18], therapistAku1, clinic1, june.AddDays(19).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[19], therapistFys1, clinic1, june.AddDays(21).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[0], therapistMassage1, clinic1, june.AddDays(22).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[1], therapistFys1, clinic1, june.AddDays(23).AddHours(10), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[2], therapistKost1, clinic1, june.AddDays(24).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[3], therapistAku1, clinic1, june.AddDays(25).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[4], therapistMassage1, clinic1, june.AddDays(26).AddHours(14), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[5], therapistFys1, clinic1, june.AddDays(27).AddHours(9), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[6], therapistKost1, clinic1, june.AddDays(28).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[7], therapistAku1, clinic1, june.AddDays(29).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[8], therapistFys1, clinic1, june.AddDays(29).AddHours(15), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[9], therapistMassage1, clinic1, june.AddDays(29).AddHours(16), 60, massage60, 699));

            // Klinik 2 - BookRight Vejle Bredballe
            bookings.Add(CreateBooking(customers[7], therapistMassage2, clinic2, june.AddDays(0).AddHours(9), 30, massage30, 350, 20, DiscountType.Campaign, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[8], therapistFys2, clinic2, june.AddDays(0).AddHours(10), 45, fys45, 589, 10, DiscountType.Loyalty, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[9], therapistKost2, clinic2, june.AddDays(1).AddHours(11), 60, kost60, 799, 0, DiscountType.None, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[10], therapistAku2, clinic2, june.AddDays(1).AddHours(13), 45, aku45, 550, 0, DiscountType.None, BookingStatus.Cancelled));
            bookings.Add(CreateBooking(customers[11], therapistMassage2, clinic2, june.AddDays(2).AddHours(14), 60, massage60, 699, 0, DiscountType.None, BookingStatus.NoShow));
            bookings.Add(CreateBooking(customers[12], therapistFys2, clinic2, june.AddDays(3).AddHours(9), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[13], therapistMassage2, clinic2, june.AddDays(4).AddHours(10), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[14], therapistKost2, clinic2, june.AddDays(7).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[15], therapistAku2, clinic2, june.AddDays(8).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[16], therapistFys2, clinic2, june.AddDays(9).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[17], therapistMassage2, clinic2, june.AddDays(10).AddHours(9), 60, massage60, 699));
            bookings.Add(CreateBooking(customers[18], therapistFys2, clinic2, june.AddDays(11).AddHours(10), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[19], therapistKost2, clinic2, june.AddDays(14).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[1], therapistAku2, clinic2, june.AddDays(15).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[2], therapistFys2, clinic2, june.AddDays(16).AddHours(14), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[3], therapistMassage2, clinic2, june.AddDays(17).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[4], therapistFys2, clinic2, june.AddDays(17).AddHours(11), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[5], therapistKost2, clinic2, june.AddDays(18).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[6], therapistAku2, clinic2, june.AddDays(19).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[7], therapistFys2, clinic2, june.AddDays(21).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[8], therapistMassage2, clinic2, june.AddDays(22).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[9], therapistFys2, clinic2, june.AddDays(23).AddHours(10), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[10], therapistKost2, clinic2, june.AddDays(24).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[11], therapistAku2, clinic2, june.AddDays(25).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[12], therapistMassage2, clinic2, june.AddDays(26).AddHours(14), 60, massage60, 699));
            bookings.Add(CreateBooking(customers[13], therapistFys2, clinic2, june.AddDays(27).AddHours(9), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[14], therapistKost2, clinic2, june.AddDays(28).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[15], therapistAku2, clinic2, june.AddDays(29).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[16], therapistFys2, clinic2, june.AddDays(29).AddHours(15), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[17], therapistMassage2, clinic2, june.AddDays(29).AddHours(16), 30, massage30, 350));

            // Klinik 3 - BookRight Egtved
            bookings.Add(CreateBooking(customers[14], therapistMassage3, clinic3, june.AddDays(0).AddHours(9), 30, massage30, 350, 0, DiscountType.None, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[15], therapistFys3, clinic3, june.AddDays(0).AddHours(10), 60, fys60, 745, 15, DiscountType.Loyalty, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[16], therapistKost3, clinic3, june.AddDays(1).AddHours(11), 60, kost60, 799, 0, DiscountType.None, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[17], therapistAku3, clinic3, june.AddDays(1).AddHours(13), 45, aku45, 550, 25, DiscountType.Birthday, BookingStatus.Completed));
            bookings.Add(CreateBooking(customers[18], therapistMassage3, clinic3, june.AddDays(2).AddHours(14), 30, massage30, 350, 0, DiscountType.None, BookingStatus.NoShow));
            bookings.Add(CreateBooking(customers[19], therapistFys3, clinic3, june.AddDays(3).AddHours(9), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[0], therapistMassage3, clinic3, june.AddDays(4).AddHours(10), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[1], therapistKost3, clinic3, june.AddDays(7).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[2], therapistAku3, clinic3, june.AddDays(8).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[3], therapistFys3, clinic3, june.AddDays(9).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[4], therapistMassage3, clinic3, june.AddDays(10).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[5], therapistFys3, clinic3, june.AddDays(11).AddHours(10), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[6], therapistKost3, clinic3, june.AddDays(14).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[7], therapistAku3, clinic3, june.AddDays(15).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[8], therapistFys3, clinic3, june.AddDays(16).AddHours(14), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[9], therapistMassage3, clinic3, june.AddDays(17).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[10], therapistFys3, clinic3, june.AddDays(17).AddHours(11), 30, fys30, 395));
            bookings.Add(CreateBooking(customers[11], therapistKost3, clinic3, june.AddDays(18).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[12], therapistAku3, clinic3, june.AddDays(19).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[13], therapistFys3, clinic3, june.AddDays(21).AddHours(15), 60, hold60, 150));

            bookings.Add(CreateBooking(customers[14], therapistMassage3, clinic3, june.AddDays(22).AddHours(9), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[15], therapistFys3, clinic3, june.AddDays(23).AddHours(10), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[16], therapistKost3, clinic3, june.AddDays(24).AddHours(11), 60, kost60, 799));
            bookings.Add(CreateBooking(customers[17], therapistAku3, clinic3, june.AddDays(25).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[18], therapistMassage3, clinic3, june.AddDays(26).AddHours(14), 30, massage30, 350));
            bookings.Add(CreateBooking(customers[19], therapistFys3, clinic3, june.AddDays(27).AddHours(9), 60, fys60, 745));
            bookings.Add(CreateBooking(customers[0], therapistKost3, clinic3, june.AddDays(28).AddHours(12), 30, kost30, 450));
            bookings.Add(CreateBooking(customers[1], therapistAku3, clinic3, june.AddDays(29).AddHours(13), 45, aku45, 550));
            bookings.Add(CreateBooking(customers[2], therapistFys3, clinic3, june.AddDays(29).AddHours(15), 45, fys45, 589));
            bookings.Add(CreateBooking(customers[3], therapistMassage3, clinic3, june.AddDays(29).AddHours(16), 30, massage30, 350));

            await _context.Bookings.AddRangeAsync(bookings);
            await _context.SaveChangesAsync();
        }
        private async Task SeedGeneratedBookingsAsync()
        {
            if (_context.Bookings.Count() > 500)
                return;

            var random = new Random();

            var therapists = _context.Therapists
            .Include(t => t.Qualifications)
            .ToList();

            var treatmentTypes = _context.TreatmentTypes.ToList();

            var clinics = _context.Clinics.ToList();
            var customers = _context.Customers.ToList();
            var noLoyaltyCustomers = customers.Take(75).ToList();

            var bronzeCustomers = customers
                .Skip(75)
                .Take(75)
                .ToList();

            var silverCustomers = customers
                .Skip(150)
                .Take(75)
                .ToList();

            var goldCustomers = customers
                .Skip(225)
                .Take(75)
                .ToList();

            var generatedBookings = new List<Booking>();
            GenerateBookingsForCustomers(
                noLoyaltyCustomers,
                2,
                5,
                generatedBookings,
                therapists,
                treatmentTypes,
                clinics,
                random);

            GenerateBookingsForCustomers(
                bronzeCustomers,
                6,
                12,
                generatedBookings,
                therapists,
                treatmentTypes,
                clinics,
                random);

            GenerateBookingsForCustomers(
                silverCustomers,
                14,
                24,
                generatedBookings,
                therapists,
                treatmentTypes,
                clinics,
                random);

            GenerateBookingsForCustomers(
                goldCustomers,
                25,
                45,
                generatedBookings,
                therapists,
                treatmentTypes,
                clinics,
                random);
            await _context.Bookings.AddRangeAsync(generatedBookings);
            await _context.SaveChangesAsync();

        }

        private List<TreatmentType> BuildTreatmentPool()
        {
            var massage30 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 30 min.");
            var massage60 = _context.TreatmentTypes.First(t => t.Name == "Sportsmassage 60 min.");
            var fys30 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 30 min.");
            var fys45 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 45 min.");
            var fys60 = _context.TreatmentTypes.First(t => t.Name == "Fysioterapi 60 min.");
            var kost60 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 60 min. førstegangskonsultation");
            var kost30 = _context.TreatmentTypes.First(t => t.Name == "Kostvejledning 30 min. opfølgning");
            var aku45 = _context.TreatmentTypes.First(t => t.Name == "Akupunktur 45 min.");
            var hold60 = _context.TreatmentTypes.First(t => t.Name == "Holdtræning/genoptræning 60 min.");

            var pool = new List<TreatmentType>();

            pool.AddRange(Enumerable.Repeat(massage30, 18));
            pool.AddRange(Enumerable.Repeat(massage60, 12));
            pool.AddRange(Enumerable.Repeat(fys30, 15));
            pool.AddRange(Enumerable.Repeat(fys45, 18));
            pool.AddRange(Enumerable.Repeat(fys60, 10));
            pool.AddRange(Enumerable.Repeat(kost60, 5));
            pool.AddRange(Enumerable.Repeat(kost30, 8));
            pool.AddRange(Enumerable.Repeat(aku45, 8));
            pool.AddRange(Enumerable.Repeat(hold60, 6));

            return pool;
        }
        private void GenerateBookingsForCustomers(
        List<Customer> customers,
        int minBookings,
        int maxBookings,
        List<Booking> generatedBookings,
        List<Therapist> therapists,
        List<TreatmentType> treatmentTypes,
        List<Clinic> clinics,
        Random random)
        {
            var treatmentPool = BuildTreatmentPool();

            foreach (var customer in customers)
            {
                var favoriteTherapist = therapists[random.Next(therapists.Count)];
                var favoriteClinic = clinics[random.Next(clinics.Count)];

                int bookingCount = random.Next(minBookings, maxBookings + 1);
                bookingCount +=
                maxBookings == 45 && random.Next(100) < 10
                    ? random.Next(20, 50)
                    : 0;

                for (int i = 0; i < bookingCount; i++)
                {
                    var therapist = random.Next(100) < 70
                    ? favoriteTherapist
                    : therapists[random.Next(therapists.Count)];

                    var clinic = random.Next(100) < 75
                    ? favoriteClinic
                    : clinics[random.Next(clinics.Count)];


                    

                    var possibleTreatmentTypes = treatmentPool
                    .Where(t => therapist.Qualifications.Any(q => q.TreatmentTypeId == t.Id))
                    .ToList();

                    if (!possibleTreatmentTypes.Any())
                        continue;

                    var treatmentType = possibleTreatmentTypes[random.Next(possibleTreatmentTypes.Count)];

                    // Tilfældig dato indenfor sidste år
                    var daysBack = random.Next(100) switch
                    {
                        < 40 => random.Next(1, 90),     // sidste 3 måneder
                        < 70 => random.Next(91, 180),   // 3-6 måneder
                        _ => random.Next(181, 365)      // 6-12 måneder
                    };

                    var startTime = DateTime.Today
                        .AddDays(-daysBack)
                        .AddHours(random.Next(8, 17))
                        .AddMinutes(random.Next(0, 4) * 15);

                    var endTime = startTime.AddMinutes(treatmentType.DurationMinutes);

                    var booking = new Booking(
                        Guid.NewGuid(),
                        customer.Id,
                        therapist.Id,
                        clinic.Id,
                        new TimeSlot(startTime, endTime)
                    );

                    var qualification = therapist.Qualifications
                    .FirstOrDefault(q => q.TreatmentTypeId == treatmentType.Id);

                    if (qualification is null)
                        continue;

                    booking.AddLine(
                        new BookingLine(
                            qualification.Id,
                            treatmentType.Price,
                            0,
                            DiscountType.None
                    ));

                    var statusRoll = random.Next(100);

                    if (statusRoll < 85)
                        booking.Complete();
                    else if (statusRoll < 95)
                        booking.Cancel();
                    else
                        booking.MarkAsNoShow();

                    generatedBookings.Add(booking);
                }
            }
        }
    }
}
