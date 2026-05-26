using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services.DiscountStrategies
{
    public class BirthdayDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _discountPercentage = 0.25m; // 25% discount
        
        public DiscountResult CalculateDiscount(PricingContext context)
        {
            var treatmentDate = DateOnly.FromDateTime(context.Booking.TimeSlot.StartTime); 
            var originalPrice = context.BasePrice;

            var bookingDate = DateOnly.FromDateTime(context.Booking.TimeSlot.StartTime);

            if (bookingDate.Month != context.Customer.DateOfBirth.Month)
                return new DiscountResult(originalPrice, originalPrice, DiscountType.None); // If the treatment date is not in the same month as the customer's birthday, return the original price with a Status of None.

            var alreadyUsedDiscount = context.CompletedBookings.Any(b =>
                b.Lines.Any(line => line.DiscountType == DiscountType.Birthday) &&           // Does any completed booking has a line with a Birthday discount?
                DateOnly.FromDateTime(b.TimeSlot.StartTime).Month == treatmentDate.Month && // Does the month of the booking's time slot matches the month of the treatment date
                DateOnly.FromDateTime(b.TimeSlot.StartTime).Year == treatmentDate.Year     // Does the year of the booking's time slot matches the year of the treatment date
            );

            if (alreadyUsedDiscount)
                return new DiscountResult(originalPrice , originalPrice, DiscountType.None); // If the customer has already used their birthday discount for this year, return the original price with a Status of None.

            // Return a DiscountResult where:
            // - originalPrice is the full price before discount
            // - discounted price is calculated by reducing the original price by 25%
            // - DiscountType.Birthday marks that the birthday discount was applied
            return new DiscountResult(
                originalPrice,
                originalPrice * (1m - _discountPercentage),
                DiscountType.Birthday);
        }


    }
}
