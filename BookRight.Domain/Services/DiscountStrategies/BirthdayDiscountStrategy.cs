using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services.DiscountStrategies
{
    public class BirthdayDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _discountPercentage = 0.75m; // 25% discount means the customer pays 75% of the original price, so we use 0.75 as the multiplier

        public DiscountResult CalculateDiscount(Customer customer, Booking booking, IEnumerable<Booking> completedBookings)
        {
            var treatmentDate = DateOnly.FromDateTime(booking.TimeSlot.StartTime); 
            var originalPrice = booking.GetTotalPrice();

            if (!customer.IsEligibleForBirthdayDiscount(treatmentDate)) 
                return new DiscountResult(originalPrice, originalPrice, DiscountType.None); // If the customer is not eligible for the birthday discount,
                                                                                            // return the original price as both the original and discounted price, along with a Status of None.


            // Calculate the discounted price by multiplying the original price by (1 - discount percentage).
            // (1 - dicount percentage) gives us the multiplier to apply to the original price to get the discounted price.
            return new DiscountResult(originalPrice, originalPrice * (1m - _discountPercentage), DiscountType.Birthday); // (1 - 0.75) = 0.25, so the customer pays 25% of the original price.
        }
    }
}
