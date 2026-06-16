using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    /// <summary>
    /// The PricingContext class encapsulates all the necessary information required to calculate the price of a booking. 
    /// This context is used by pricing services to determine the final price after applying all relevant discounts and promotions.
    /// Reason it's here is due to EF Core's thread safety issues with entities, so we need a separate context to pass to the pricing service without risking concurrency issues.
    /// </summary>
    public record PricingContext
    {
        public Customer Customer { get; init; } = default!;
        public Booking Booking { get; init; } = default!;
        public IEnumerable<Booking> CompletedBookings { get; init; } = default!;
        public CampaignDiscount? CampaignDiscount { get; init; } // Nullable, because not all bookings will have a campaign discount applied
        public Money BasePrice { get; init; } = default!;
        public Money MostExpensiveLinePrice { get; init; } = default!;
        public bool BirthdayDiscountAssigned { get; init; } = false;

    }
}
