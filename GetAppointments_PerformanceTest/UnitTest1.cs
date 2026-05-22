using Bogus;
using BookRight.Domain.Aggregates;
using BookRight.Domain.ValueObjects;
using BookRight.Infrastructure;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Persistence.Repositories;
using BookRight.UseCases;
using BookRight.UseCases.GetAllCustomers;
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

        public PerformanceTests()
        {
            var connectionstring = "Server=localhost;Database=BookRightDb;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<BookRightDbContext>()
                .UseSqlServer(connectionstring)
                .Options;

            _context = new BookRightDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task SystemResponseTime()
        {
            if (!await _context.Customers.AnyAsync())
            {
                // 1. Generer 3 klinikker og gem dem i en variabel ('clinics')
                var clinicFaker = new Faker<ClinicEntity>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => f.Company.CompanyName());

                var clinics = clinicFaker.Generate(3);
                await _context.Clinics.AddRangeAsync(clinics);
                await _context.SaveChangesAsync();


                // 2. Generer 12 behandlere
                var therapistFaker = new Faker<TherapistEntity>()
                    .RuleFor(t => t.Id, f => Guid.NewGuid())
                    .RuleFor(t => t.Name, f => new FullName(f.Name.FirstName(), f.Name.LastName()))
                    .RuleFor(t => t.ClinicId, f => f.PickRandom(clinics).Id);

                await _context.Therapists.AddRangeAsync(therapistFaker.Generate(12));
                await _context.SaveChangesAsync();


                // 3. Generer 400 kunder
                var customerFaker = new Faker<CustomerEntity>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => new FullName(f.Name.FirstName(), f.Name.LastName()))
                    .RuleFor(c => c.Phone, f => new PhoneNumber(f.Phone.PhoneNumber()));

                await _context.Customers.AddRangeAsync(customerFaker.Generate(400));
                await _context.SaveChangesAsync();
            }


            var customerRepository = new CustomerRepository(_context);

            var useCase = new GetAllCustomersUseCase(customerRepository);

            await useCase.ExecuteAsync();

            var stopwatch = new Stopwatch();
            int iterations = 5;
            long totalTime = 0;

            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Restart();

                var result = await useCase.ExecuteAsync();

                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
            }

            long averageTime = totalTime / iterations;

            Assert.True(averageTime < 500, $"Responstid var for høj. Gennemsnit: {averageTime}ms");

            // averageTime står til at være under 500ms. Kan ændres til 2000ms, for 2 sekunders responstid, hvis det er nødvendigt.
        }
    }
}