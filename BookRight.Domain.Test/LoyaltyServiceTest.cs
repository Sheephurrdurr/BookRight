using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;
using BookRight.Domain.ValueObjects;
using Xunit;

namespace BookRight.Domain.Test
{
    public class LoyaltyServiceTest
    {
        private readonly LoyaltyService _service = new();

        // Test 1: Null bookings should throw exception
        [Fact]
        public void CalculateTotalPurchasesLast12Months_NullBookings_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _service.CalculateTotalPurchasesLast12Months(null!, DateTime.Today));
        }

        // Test 2: Empty booking list should return zero
        [Fact]
        public void CalculateTotalPurchasesLast12Months_NoBookings_ShouldReturnZero()
        {
            // Arrange
            var bookings = new List<Booking>();

            // Act
            var result = _service.CalculateTotalPurchasesLast12Months(bookings, DateTime.Today);

            // Assert
            Assert.Equal(new Money(0), result);
        }

        // Test 3: Customer below 3000 kr. should get no loyalty level
        [Fact]
        public void GetLoyaltyLevel_TotalBelow3000_ShouldReturnNone()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2); //Fake CurrentDate. Add 2 years to ensure the date is still in the future because of currentDate.AddMonths(-12).
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 2999)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.None, result);
        }

        // Test 4: Customer with 3000 kr. should get Bronze
        [Fact]
        public void GetLoyaltyLevel_Total3000_ShouldReturnBronze()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 3000)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.Bronze, result);
        }

        // Test 5: Customer with 10000 kr. should still get Bronze
        [Fact]
        public void GetLoyaltyLevel_Total10000_ShouldReturnBronze()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 10000)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.Bronze, result);
        }

        // Test 6: Customer above 10000 kr. should get Silver
        [Fact]
        public void GetLoyaltyLevel_TotalAbove10000_ShouldReturnSilver()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 10001)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.Silver, result);
        }

        // Test 7: Customer with 25000 kr. should still get Silver
        [Fact]
        public void GetLoyaltyLevel_Total25000_ShouldReturnSilver()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 25000)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.Silver, result);
        }

        // Test 8: Customer above 25000 kr. should get Gold
        [Fact]
        public void GetLoyaltyLevel_TotalAbove25000_ShouldReturnGold()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);
            var bookings = new List<Booking>
            {
                CreateCompletedBooking(currentDate.AddDays(-1), 25001)
            };

            // Act
            var result = _service.GetLoyaltyLevel(bookings, currentDate);

            // Assert
            Assert.Equal(LoyaltyLevel.Gold, result);
        }

        // Test 9: Only completed bookings should count
        [Fact]
        public void CalculateTotalPurchasesLast12Months_OnlyCompletedBookings_ShouldBeIncluded()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);

            var completedBooking = CreateBooking(currentDate.AddDays(-1), 1000);
            completedBooking.Complete();

            var cancelledBooking = CreateBooking(currentDate.AddDays(-1), 1000);
            cancelledBooking.Cancel();

            var noShowBooking = CreateBooking(currentDate.AddDays(-1), 1000);
            noShowBooking.MarkAsNOShow();

            var bookings = new List<Booking>
            {
                completedBooking,
                cancelledBooking,
                noShowBooking
            };

            // Act
            var result = _service.CalculateTotalPurchasesLast12Months(bookings, currentDate);

            // Assert
            Assert.Equal(new Money(1000), result);
        }

        // Test 10: Only bookings within the last 12 months should count
        [Fact]
        public void CalculateTotalPurchasesLast12Months_OnlyBookingsWithinLast12Months_ShouldBeIncluded()
        {
            // Arrange
            var currentDate = DateTime.Today.AddYears(2);

            var bookingExactly12MonthsOld =
                CreateCompletedBooking(currentDate.AddMonths(-12), 1000);

            var bookingOlderThan12Months =
                CreateCompletedBooking(currentDate.AddMonths(-12).AddDays(-1), 1000);

            var recentBooking =
                CreateCompletedBooking(currentDate.AddDays(-1), 1000);

            var bookings = new List<Booking>
            {
                bookingExactly12MonthsOld,
                bookingOlderThan12Months,
                recentBooking
            };

            // Act
            var result = _service.CalculateTotalPurchasesLast12Months(bookings, currentDate);

            // Assert
            Assert.Equal(new Money(2000), result);
        }

        // Helper method for creating completed bookings
        private static Booking CreateCompletedBooking(DateTime startTime, decimal totalPrice)
        {
            var booking = CreateBooking(startTime, totalPrice);

            booking.Complete();

            return booking;
        }

        // Helper method for creating bookings
        private static Booking CreateBooking(DateTime startTime, decimal totalPrice)
        {
            var booking = new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new TimeSlot(
                    startTime,
                    startTime.AddMinutes(60))
            );

            booking.AddLine(CreateBookingLine(totalPrice));

            return booking;
        }

        // Helper method for creating booking lines
        private static BookingLine CreateBookingLine(decimal price)
        {
            return new BookingLine(
                Guid.NewGuid(),
                new Money(price),
                0,
                DiscountType.None
            );
        }
    }
}