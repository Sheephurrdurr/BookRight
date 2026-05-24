using BookRight.Domain.Aggregates.AddOn;
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
            var treatmentType = new TreatmentType("Massage", 60, 1, new Money(300));
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

    }
}
