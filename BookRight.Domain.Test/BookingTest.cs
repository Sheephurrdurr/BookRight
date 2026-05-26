using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.ValueObjects;
using Xunit;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Domain.Aggregates.CampaignDiscount;
namespace BookRight.Domain.Test
{
	public class BookingTest
	{
		// Test 1 : Booking kan oprettes korrekt
		[Fact]
		public void Create_ValidBooking_ShouldSucceed()
		{
			//Arrange
			var id = Guid.NewGuid();
			var customerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
			var clinicId = Guid.NewGuid();
			var timeSlot = new TimeSlot(
			   DateTime.Today.AddDays(1).AddHours(10),
			   DateTime.Today.AddDays(1).AddHours(11));


			// Act
			var booking = new Booking(id, customerId, clinicId, therapistId, timeSlot);

			//Assert
			Assert.NotNull(booking);

		}

        // Test 2: Booking må ikke oprettes uden CustomerId
        [Fact]
        public void Create_EmptyCustomerId_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Empty Guid to test validation
            var customerId = Guid.Empty;
            var therapistId = Guid.Empty;

            var clinicId = Guid.NewGuid();

            var timeSlot = new TimeSlot(
                DateTime.Today.AddDays(1).AddHours(10),
                DateTime.Today.AddDays(1).AddHours(11));

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Booking(id, customerId, clinicId, therapistId, timeSlot));
        }

        // Test 3: Booking må ikke oprettes uden ClinicId
        [Fact]
        public void Create_EmptyClinicId_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            // Empty Guid to test validation
            var clinicId = Guid.Empty;
            var therapistId = Guid.Empty;

            var timeSlot = new TimeSlot(
                DateTime.Today.AddDays(1).AddHours(10),
                DateTime.Today.AddDays(1).AddHours(11));

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Booking(id, customerId, clinicId,therapistId, timeSlot));
        }

        // Test 4: Booking må ikke oprettes uden TimeSlot
        [Fact]
        public void Create_NullTimeSlot_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();

            TimeSlot? timeSlot = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Booking(id, customerId, therapistId, clinicId, timeSlot!));
        }

        // Test 5: Ny booking får status Confirmed
        [Fact]
        public void Create_ValidBooking_ShouldHaveConfirmedStatus()
        {
            // Arrange
            var booking = CreateValidBooking();

            // Act & Assert
            Assert.Equal(BookingStatus.Confirmed, booking.Status);
        }

        // Test 6: Booking kan annulleres
        [Fact]
        public void Cancel_Booking_ShouldSetStatusToCancelled()
        {
            // Arrange
            var booking = CreateValidBooking();

            // Act
            booking.Cancel();

            // Assert
            Assert.Equal(BookingStatus.Cancelled, booking.Status);
        }

        // Test 7: Booking må ikke få tom CampaignDiscountId
        [Fact]
        public void ApplyCampaignDiscount_EmptyId_ShouldThrowException()
        {
            // Arrange
            var booking = CreateValidBooking();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                booking.ApplyCampaignDiscount(Guid.Empty));
        }

        //Test 8: Booking pris returnerer rigtig rabat %
        [Fact]
        public void CalculateDiscount_ApplyCorrectPercentage()
        {
            //Arrange
            var strategy = new CampaignDiscountStrategy();

            var customer = new Customer(
                new FullName("Test", "Tester"),
                new Email("test@dk"),
                new PhoneNumber("10101010"),
                new DateOnly(2003, 10, 10),
                healthNotes: string.Empty,
                preferredTherapistId: null
                );

            var booking = CreateValidBooking();

            var campaign = new CampaignDiscount(
                "Test-Kampagne",
                15m,
                new DateRange(
                    DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                    DateOnly.FromDateTime(DateTime.Today.AddDays(30))),
                new List<Guid> { Guid.NewGuid() });


            var pricingContext = new PricingContext
            {
                Customer = customer,
                Booking = booking,
                CompletedBookings = Enumerable.Empty<Booking>(),
                CampaignDiscount = campaign,
                BasePrice = new Money(100m)
            };

            //Act
            var results = strategy.CalculateDiscount(pricingContext);

            //Assert
            Assert.Equal(100m, results.OriginalPrice.Value);
            Assert.Equal(85m, results.DiscountedPrice.Value);
            Assert.Equal("15% rabat", results.DiscountName);
        }
        // Hjælpemetoden som opretter en valid Booking med gyldige testdata
        private static Booking CreateValidBooking()
        {
            var timeSlot = new TimeSlot(
                DateTime.Today.AddDays(1).AddHours(10),
                DateTime.Today.AddDays(1).AddHours(11));

            return new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                timeSlot);
        }

    }

}
	