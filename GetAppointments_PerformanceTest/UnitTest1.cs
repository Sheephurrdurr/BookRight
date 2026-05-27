using Bogus;
using BookRight.Domain.ValueObjects;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Persistence.Repositories;
using BookRight.UseCases.GetAllCustomers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Xunit;
// Aliases for at undgå navnesammenstød med mapperne
using ClinicEntity = BookRight.Domain.Aggregates.Clinic.Clinic;
using CustomerEntity = BookRight.Domain.Aggregates.Customer.Customer;
using TherapistEntity = BookRight.Domain.Aggregates.TherapistAggregate.Therapist;

namespace GetAppointments_PerformanceTest
{
    public class PerformanceTests
    {
        private readonly BookRightDbContext _context;
        private readonly SqliteConnection _connection;
        public PerformanceTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BookRightDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BookRightDbContext(options);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
            _connection.Dispose();
        }

        [Fact]
        public async Task SystemResponseTime()
        {
            // ARRANGE
            // Generer 3 klinikker og gem dem i en variabel ('clinics')
            var clinicFaker = new Faker<ClinicEntity>()
                .CustomInstantiator(f => new ClinicEntity(
                    name: f.Company.CompanyName(),
                    address: new Address(
                        street: f.Address.StreetAddress(),
                        city: f.Address.City(),
                        postalCode: f.Address.ZipCode()
                    ),
                    phone: new PhoneNumber(f.Phone.PhoneNumber("#########")),
                    numTreatmentRooms: f.Random.Int(1, 10)
               ));


            var clinics = clinicFaker.Generate(3);
            await _context.Clinics.AddRangeAsync(clinics);
            await _context.SaveChangesAsync();


            //Generer 12 behandlere
            var therapistFaker = new Faker<TherapistEntity>()
                   .CustomInstantiator(f => new TherapistEntity(
                       name: new FullName(f.Name.FirstName(), f.Name.LastName()),
                       email: new Email(f.Internet.Email()),
                       specialization: f.PickRandom("Massør", "Fysioterapeut", "Akupunktør"),
                       authorization: new Authorization("Autoriseret fysioterapeut", "FYS-1001"),
                       clinicId: f.PickRandom(clinics).Id
                       ));

            await _context.Therapists.AddRangeAsync(therapistFaker.Generate(12));
            await _context.SaveChangesAsync();


            //Generer 400 kunder
            var customerFaker = new Faker<CustomerEntity>()
                .CustomInstantiator(f => new CustomerEntity(
                   name: new FullName(f.Name.FirstName(), f.Name.LastName()),
                    email: new Email(f.Internet.Email()),
                    phone: new PhoneNumber(f.Phone.PhoneNumber("#########")),
                    dateOfBirth: DateOnly.FromDateTime(
                        f.Date.Between(DateTime.Now.AddYears(-70), DateTime.Now.AddYears(-18))
                        ),
                    healthNotes: f.Random.Bool() ? f.Lorem.Sentence() : null,
                    preferredTherapistId: null
                   ));

            await _context.Customers.AddRangeAsync(customerFaker.Generate(400));
            await _context.SaveChangesAsync();


            // ACT
            var customerRepository = new CustomerRepository(_context);

            var useCase = new GetAllCustomersUseCase(customerRepository);

            await useCase.ExecuteAsync();

            var stopwatch = new Stopwatch();
            int iterations = 5;
            long totalTime = 0;

            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Restart();
                await useCase.ExecuteAsync();

                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
            }

            long averageTime = totalTime / iterations;

            Assert.True(averageTime < 500, $"Responstid var for høj. Gennemsnit: {averageTime}ms");

            // averageTime står til at være under 500ms. Kan ændres til 2000ms, for 2 sekunders responstid, hvis det er nødvendigt.
        }
    }
}