using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.ValueObjects;
using Xunit;

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
			var clinicId = Guid.NewGuid();
			var timeSlot = new TimeSlot(
			   DateTime.Today.AddDays(1).AddHours(10),
			   DateTime.Today.AddDays(1).AddHours(11));


			// Act
			var booking = new Booking(id, customerId, clinicId, timeSlot);

			//Assert
			Assert.NotNull(booking);

		}

		// Test 2: Booking må ikke oprettes uden CusotmerId
		[Fact]
		public void Create_EmptyCustomerId_ShouldThrowException()
		{
			//Arrange
			var id = Guid.NewGuid();
			var customerId = Guid.NewGuid();
			var clinicId = Guid.NewGuid();
			var timeSlot = new TimeSlot(
			DateTime.Today.AddDays(1).AddHours(10),
			DateTime.Today.AddDays(1).AddHours(11));

			//Act & Assert
			Assert.Throws<ArgumentException>(() =>
			  new Booking(id, customerId, clinicId, timeSlot));

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

            var timeSlot = new TimeSlot(
                DateTime.Today.AddDays(1).AddHours(10),
                DateTime.Today.AddDays(1).AddHours(11));

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Booking(id, customerId, clinicId, timeSlot));
        }

        // Test 4: Booking må ikke oprettes uden TimeSlot
        [Fact]
		public void Create_NullTimeSlot_ShouldThrowException()
		{
			var id = Guid.NewGuid();
			var customerId = Guid.NewGuid();
			var clinicId = Guid.NewGuid();

			// Act & Assert
			Assert.Throws<ArgumentException>(() =>
			  new Booking(id, customerId, clinicId, null));
			
			
		}



	}

}
	