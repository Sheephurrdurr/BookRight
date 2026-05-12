using BookRight.Domain.Aggregates;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Services;

namespace BookRight.Domain.Test
{
    //Herinde tester du dine metoder, act arrange assert, 
    public class CustomerNameTest
    {
        [Fact]
        public void Customer_LastName_Has_WhiteSpace_Throws_ArguementException() //Unhappy path
        {
            //Arrange, Act & Assert
            var exception = Assert.Throws<ArgumentException>
                 (() => new FullName("Cate", " "));
        }
    }

    public class PriceCalculatorTests
    {
        [Fact]
        public void CalculateBasePrice_ReturnsTreatmentPrice()
        {
            // Arrange
            var treatmentType = new TreatmentType("Massage", 60, 1, new Money(300));
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.CalculateBasePrice(treatmentType);

            // Assert
            Assert.Equal(new Money(300), result);


        }

        [Fact]
        public void ApplyAddOns_AddsAllAddOnPrices()
        {
            // Arrange
            var basePrise = new Money(300);
            var addOns = new List<AddOn>
            {
                new AddOn("Fysioterapi", new Money(50)),
                new AddOn("Akupunktur", new Money(70))
            };
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.ApplyAddOns(basePrise, addOns);

            // Assert
            Assert.Equal(new Money(425), result);
        }

        [Fact]
        public void ApplyAddOns_NoAddOns_ReturnsBasePrice()
        {
            // Arrange
            var basePrise = new Money(300);
            var addOns = new List<AddOn>();
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.ApplyAddOns(basePrise, addOns);

            // Assert
            Assert.Equal(new Money(300), result);
        }

        [Fact]
        public void ApplyDiscount_AppliesPercentageCorrectly()
        {
            // Arrange
            var basePrice = new Money(400);
            decimal percentage = 0.10m; // 10%
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.ApplyDiscount(basePrice, percentage);

            // Assert
            Assert.Equal(new Money(400), result.OriginalPrice);
            Assert.Equal(new Money(360), result.DiscountedPrice);
        }

        [Fact]
        public void ApplyDiscount_ZeroPercent_ReturnsSamePrice()
        {
            // Arrange
            var basePrice = new Money(400);
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.ApplyDiscount(basePrice, 0);

            // Assert
            Assert.Equal(new Money(400), result.DiscountedPrice);
            Assert.Equal("10% rabat", result.DiscountName);
        }

    }
}
