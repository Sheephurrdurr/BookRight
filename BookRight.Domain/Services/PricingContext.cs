using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.Domain.Aggregates.Customer;

namespace BookRight.Domain.Services
{
    public record PricingContext
    {
        public Customer Customer { get; init; } = default!;
        public Booking Booking { get; init; } = default!;
        public IEnumerable<Booking> CompletedBookings { get; init; } = default!;
        public CampaignDiscount? CampaignDiscount { get; init; } // Nullable, because not all bookings will have a campaign discount applied    

    }
}
