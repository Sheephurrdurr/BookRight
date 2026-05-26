using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Test
{
    //Herinde tester du dine metoder, act arrange assert, 


    public class PriceCalculatorTests
    {
        [Fact]
        public void CalculateBasePrice_ReturnsTreatmentPrice()
        {
            // Arrange
            var treatmentType = new TreatmentType("Massage", 60, 1, new Money(300), true);
            var calculator = CreateCalculator();

            // Act
            var result = calculator.CalculateBasePrice(treatmentType);

            // Assert
            Assert.Equal(new Money(300), result);


        }

        [Fact]
        public void ApplyAddOns_AddsAllAddOnPercentages()
        {
            // Arrange
            var basePrice = new Money(300);

            var addOns = new List<AddOn>
            {
             // 10% of 300 = 30
                new AddOn("Evening surcharge", 10),

                // 15% of 300 = 45
                new AddOn("Weekend surcharge", 15)
            };

            var calculator = CreateCalculator();

            // Act
            var result = calculator.ApplyAddOns(basePrice, addOns);

            // Assert
            // 300 + 30 + 45 = 375
            Assert.Equal(new Money(375), result);
        }

        [Fact]
        public void ApplyAddOns_NoAddOns_ReturnsBasePrice()
        {
            // Arrange
            var basePrice = new Money(300);
            var addOns = new List<AddOn>();
            var calculator = CreateCalculator();

            // Act
            var result = calculator.ApplyAddOns(basePrice, addOns);

            // Assert
            Assert.Equal(new Money(300), result);
        }

        [Fact]
        public void ApplyDiscount_AppliesPercentageCorrectly()
        {
            // Arrange
            var basePrice = new Money(400);

            // 10% discount
            decimal percentage = 10;

            var calculator = CreateCalculator();

            // Act
            var result = calculator.ApplyDiscount(basePrice, percentage, DiscountType.Campaign);

            // Assert
            Assert.Equal(new Money(400), result.OriginalPrice);

            // 400 - 10% = 360
            Assert.Equal(new Money(360), result.DiscountedPrice);

            Assert.Equal("10% rabat", result.DiscountName);
        }

        [Fact]
        public void ApplyDiscount_ZeroPercent_ReturnsSamePrice()
        {
            // Arrange
            var basePrice = new Money(400);

            var calculator = CreateCalculator();

            // Act
            var result = calculator.ApplyDiscount(basePrice, 0, DiscountType.None);

            // Assert
            // Price should remain unchanged when discount is 0%
            Assert.Equal(new Money(400), result.DiscountedPrice);

            // Discount name should reflect the applied percentage
            Assert.Equal("0% rabat", result.DiscountName);
        }

        [Fact]
        public void ApplyAddOns_WithSingle15PercentSurcharge_Adds15Percent()
        {
            var calculator = CreateCalculator();

            var basePrice = new Money(300);
            var addOns = new List<AddOn>
            {
                new AddOn("Weekend surcharge", 15)
            };

            var result = calculator.ApplyAddOns(basePrice, addOns);

            Assert.Equal(new Money(345), result);
        }

        [Fact]
        public void ApplyDiscount_With100Percent_ReturnsZero()
        {
            var calculator = CreateCalculator();
            var basePrice = new Money(400);

            var result = calculator.ApplyDiscount(basePrice, 100, DiscountType.None);

            Assert.Equal(new Money(0), result.DiscountedPrice);
        }

        // Helper method to create a PriceCalculatorService with no discount strategies for testing
        private static PriceCalculatorService CreateCalculator()
        {
            return new PriceCalculatorService(new List<IDiscountStrategy>());
        }

        // Helper method to create a sample customer for testing
        [Fact]
        public async Task CalculateBestDiscountAsync_ReturnsLowestDiscountedPrice()
        {
            // Arrange
            var strategies = new List<IDiscountStrategy>
    {
        new FakeDiscountStrategy(new Money(100), new Money(90), DiscountType.Loyalty),
        new FakeDiscountStrategy(new Money(100), new Money(75), DiscountType.Birthday),
        new FakeDiscountStrategy(new Money(100), new Money(80), DiscountType.Campaign)
    };

            var calculator = new PriceCalculatorService(strategies);

            var customer = CreateCustomer();
            var booking = CreateBooking();
            var completedBookings = new List<Booking>();

            var pricingContext = new PricingContext
            {
                Customer = customer,
                Booking = booking,
                CompletedBookings = completedBookings,
                CampaignDiscount = null
            };

            // Act
            var result = await calculator.CalculateBestDiscountAsync(pricingContext);

            // Assert
            Assert.Equal(new Money(75), result.DiscountedPrice);
            Assert.Equal(DiscountType.Birthday, result.AppliedDiscount);
        }

        private class FakeDiscountStrategy : IDiscountStrategy
        {
            private readonly DiscountResult _result;

            public FakeDiscountStrategy(Money originalPrice, Money discountedPrice, DiscountType discountType)
            {
                _result = new DiscountResult(originalPrice, discountedPrice, discountType);
            }

            public DiscountResult CalculateDiscount(PricingContext context)
            {
                return _result;
            }
        }

        // Tests that a weekend booking gets a 15% automatic add-on.
        [Fact]
        public void GetAutomaticAddOns_Weekend_Returns15PercentAddOn()
        {
            // Arrange
            var calculator = CreateCalculator();

            var timeSlot = new TimeSlot(
                new DateTime(2027, 5, 8, 10, 0, 0), // Saturday
                new DateTime(2027, 5, 8, 11, 0, 0));

            // Act
            var result = calculator.GetAutomaticAddOns(timeSlot).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Aften-/weekendtillæg", result[0].Name);
            Assert.Equal(15, result[0].Percentage);
        }

        // Tests that an evening booking gets a 15% automatic add-on.
        [Fact]
        public void GetAutomaticAddOns_Evening_Returns15PercentAddOn()
        {
            // Arrange
            var calculator = CreateCalculator();

            var timeSlot = new TimeSlot(
                new DateTime(2027, 5, 10, 18, 0, 0), // Monday evening
                new DateTime(2027, 5, 10, 19, 0, 0));

            // Act
            var result = calculator.GetAutomaticAddOns(timeSlot).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Aften-/weekendtillæg", result[0].Name);
            Assert.Equal(15, result[0].Percentage);
        }

        // Tests that a normal weekday booking does not get any automatic add-ons.
        [Fact]
        public void GetAutomaticAddOns_NormalWeekday_ReturnsNoAddOns()
        {
            // Arrange
            var calculator = CreateCalculator();

            var timeSlot = new TimeSlot(
                new DateTime(2027, 5, 10, 10, 0, 0), // Monday daytime
                new DateTime(2027, 5, 10, 11, 0, 0));

            // Act
            var result = calculator.GetAutomaticAddOns(timeSlot).ToList();

            // Assert
            Assert.Empty(result);
        }


        // Helper method to create a test booking
        private static Booking CreateBooking()
        {
            var startTime = DateTime.Now.AddDays(1);
            var endTime = startTime.AddHours(1);

            return new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new TimeSlot(startTime, endTime));
        }

        // Helper method to create a test customer
        private static Customer CreateCustomer()
        {
            return new Customer(
                new FullName("Test", "Customer"),
                new Email("test@test.dk"),
                new PhoneNumber("12345678"),
                new DateOnly(1990, 5, 1),
                string.Empty,
                null);
        }
    }
}
