
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services.DiscountStrategies
{
    public class LoyaltyDiscountStrategy : IDiscountStrategy
    {
        private readonly LoyaltyService _loyaltyService; // Underscore for private field

        public LoyaltyDiscountStrategy(LoyaltyService loyaltyService)
        {
            _loyaltyService = loyaltyService; // Dependency injection of LoyaltyService
        }

        // Calculates the discount based on the customer's loyalty level, which is determined by their completed bookings in the last 12 months.
        public DiscountResult CalculateDiscount(
            Customer customer, 
            Booking booking,
            IEnumerable<Booking> completedBookings)
        {
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now); // Use service method to determine loyalty level of the customer

            // Set discount multiplier based on loyalty level. Higher levels get a bigger discount.
            // We multiply the original price by the multiplier to get the discounted price. If the multiplier is 1.0, it means no discount is applied.
            var multiplier = loyaltyLevel switch
            {
                LoyaltyLevelType.None => 1.0m,
                LoyaltyLevelType.Bronze => 0.95m,
                LoyaltyLevelType.Silver => 0.90m,
                LoyaltyLevelType.Gold => 0.85m,
                _                   => 1.0m
            };

            var originalPrice = booking.GetTotalPrice(); // Get the original price of the booking before applying any discounts, via a method on the Booking aggregate

            if (multiplier == 1.0m) 
                return new DiscountResult(originalPrice, originalPrice, DiscountType.None);// If no discount is applied, return the original price as both the original and discounted price,
                                                                                          // with a discount name indicating no discount

            var discountedPrice = originalPrice * multiplier; // Calculate the discounted price by multiplying the original price by the discount multiplier.

            return new DiscountResult(originalPrice, discountedPrice, DiscountType.Loyalty); 
        }
    }
}
