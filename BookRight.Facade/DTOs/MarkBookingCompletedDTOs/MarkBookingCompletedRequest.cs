namespace BookRight.Facade.DTOs.MarkBookingCompletedDTOs
{
    public record MarkBookingCompletedRequest
    {
        public Guid BookingId { get; set; }
    }
}
