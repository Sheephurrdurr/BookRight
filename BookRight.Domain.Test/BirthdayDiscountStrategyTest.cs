using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Domain.ValueObjects;
using Xunit;

namespace BookRight.Domain.Test
{
    public class BirthdayDiscountStrategyTest
    {
        [Fact]
        public void CalculateDiscount_NotUsedBefore_ShouldReturn25PercentDiscount()
        {
            // Arrange: Create the strategy and test data.
            var strategy = new BirthdayDiscountStrategy();
            var customer = CreateCustomer();

            var birthdayMonth = customer.DateOfBirth.Month;
            // Create a booking in the future with a price of 100 kr.
            var booking = CreateBooking(
                new DateTime(DateTime.Today.Year + 1, birthdayMonth, 15, 10, 0, 0),
                100m);

            // No previous completed bookings, so birthday discount has not been used.
            var completedBookings = new List<Booking>();

            var pricingContext = new PricingContext
            {
                Customer = customer,
                Booking = booking,
                CompletedBookings = completedBookings,
                CampaignDiscount = null,
                BasePrice = new Money(100m),
                MostExpensiveLinePrice = new Money(100m),
                BirthdayDiscountAssigned = false
            };

            // Act: Calculate the discount.
            var result = strategy.CalculateDiscount(pricingContext);

            // Assert: Original price is 100 kr.
            Assert.Equal(new Money(100m), result.OriginalPrice);

            // Assert: 25% discount means the customer pays 75 kr.
            Assert.Equal(new Money(75m), result.DiscountedPrice);

            // Assert: Birthday discount was applied.
            Assert.Equal(DiscountType.Birthday, result.AppliedDiscount);
        }

        [Fact]
        public void CalculateDiscount_AlreadyUsedThisMonth_ShouldReturnNoDiscount()
        {
            // Arrange: Create the strategy and test customer.
            var strategy = new BirthdayDiscountStrategy();
            var customer = CreateCustomer();

            // Use a future date to avoid TimeSlot validation problems.
            var bookingDate = DateTime.Today.AddYears(1).AddMonths(1).AddHours(10);

            // This is the new booking where we try to apply birthday discount.
            var booking = CreateBooking(bookingDate, 100m);

            // Create a previous completed booking in the same month and year.
            var completedBooking = CreateBooking(bookingDate.AddDays(-1), 100m);

            // Add a booking line showing that birthday discount was already used.
            completedBooking.AddLine(
                new BookingLine(
                    Guid.NewGuid(),
                    new Money(100m),
                    25m,
                    DiscountType.Birthday));

            // Mark the previous booking as completed.
            completedBooking.Complete();

            // Put the completed booking into the booking history.
            var completedBookings = new List<Booking>
            {
                completedBooking
            };

            var pricingContext = new PricingContext
            {
                Customer = customer,
                Booking = booking,
                CompletedBookings = completedBookings,
                CampaignDiscount = null,
                BasePrice = new Money(100m)
            };

            // Act: Calculate the discount.
            var result = strategy.CalculateDiscount(pricingContext);

            // Assert: Original price is still 100 kr.
            Assert.Equal(new Money(100m), result.OriginalPrice);

            // Assert: No discount, so discounted price is also 100 kr.
            Assert.Equal(new Money(100m), result.DiscountedPrice);

            // Assert: No discount should be applied.
            Assert.Equal(DiscountType.None, result.AppliedDiscount);
        }

        private static Customer CreateCustomer()
        {
            // Helper method to create a valid test customer.
            return new Customer(
                new FullName("Test", "Tester"),
                new Email("test@test.dk"),
                new PhoneNumber("12345678"),
                new DateOnly(1990, 5, 1),
                healthNotes: string.Empty,
                preferredTherapistId: null);
        }

        private static Booking CreateBooking(DateTime startTime, decimal price)
        {
            // Helper method to create a valid booking with one booking line.
            var booking = new Booking(
                Guid.NewGuid(), // CustomerId
                Guid.NewGuid(), // TherapistId
                Guid.NewGuid(), // ClinicId
                Guid.NewGuid(), //TherapistId
                new TimeSlot(startTime, startTime.AddHours(1)));

            // Add one line to the booking with the given price.
            booking.AddLine(
                new BookingLine(
                    Guid.NewGuid(),
                    new Money(price),
                    0m,
                    DiscountType.None));

            return booking;
        }
    }
}