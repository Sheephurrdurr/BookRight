using Bogus;
using BookRight.Domain.Aggregates;
using BookRight.Infrastructure;
using BookRight.Infrastructure.Persistence;
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
                    .RuleFor(c => c.Id, () => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => f.Company.CompanyName());

                var clinics = clinicFaker.Generate(3); // <-- FIX: Nu findes listen!
                await _context.Clinics.AddRangeAsync(clinics);
                await _context.SaveChangesAsync();

                // 2. Generer 12 behandlere
                var therapistFaker = new Faker<TherapistEntity>()
                    .RuleFor(t => t.Id, () => Guid.NewGuid())
                    .RuleFor(t => t.Name, f => f.Name.FullName())
                    .RuleFor(t => t.ClinicId, f => f.PickRandom(clinics).Id); // <-- FIX: Nu virker clinics her

                await _context.Therapists.AddRangeAsync(therapistFaker.Generate(12));
                await _context.SaveChangesAsync();

                // 3. Generer 400 kunder
                var customerFaker = new Faker<CustomerEntity>()
                    .RuleFor(c => c.Id, () => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => f.Name.FullName())
                    .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber());

                await _context.Customers.AddRangeAsync(customerFaker.Generate(400));
                await _context.SaveChangesAsync();
            }

            // 4. INSTANTIÉR JERES USE CASE
            // Hvis denne stadig er rød, så tryk 'Alt + Enter' på den for at se om den skal hedde 
            // noget andet, eller om den mangler en specifk under-using (f.eks. BookRight.UseCases.CreateCustomer)
            var useCase = new GetAllCustomersUseCase(_context);

            // WARM UP (Første kald tæller ikke, da EF Core skal varme op)
            await useCase.HandleAsync();

            // 5. SELVE TESTEN
            var stopwatch = new Stopwatch();
            int iterations = 5;
            long totalTime = 0;

            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Restart();

                var result = await useCase.HandleAsync();

                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
            }

            long averageTime = totalTime / iterations;

            Assert.True(averageTime < 500, $"Responstid var for høj. Gennemsnit: {averageTime}ms");
        }
    }
}