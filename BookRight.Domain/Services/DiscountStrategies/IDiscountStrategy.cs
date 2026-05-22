
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services.DiscountStrategies
{
    public interface IDiscountStrategy
    {
        // Calculates the discount for a given booking and customer, based on the customer's completed bookings.
        // Interface has 3 implementations: BirthdayDiscountStrategy, LoyaltyDiscountStrategy, CampaignDiscountStrategy
        DiscountResult CalculateDiscount(
            Customer customer,
            Booking booking,
            IEnumerable<Booking> completedBookings);
    }
}
