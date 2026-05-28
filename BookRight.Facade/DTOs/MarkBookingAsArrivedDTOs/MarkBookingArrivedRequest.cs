
namespace BookRight.Facade.DTOs.MarkBookingAsArrivedDTOs
{
    public sealed record MarkBookingArrivedRequest
    {
        public Guid BookingId { get; set; }
    }
}
