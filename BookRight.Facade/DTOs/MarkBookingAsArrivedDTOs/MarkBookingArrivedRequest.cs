
namespace BookRight.Facade.DTOs.MarkBookingAsArrivedDTOs
{
    public record MarkBookingArrivedRequest
    {
        public Guid BookingId { get; set; }
    }
}
