namespace BookRight.Facade.DTOs.MarkBookingCompleted
{
    public record MarkBookingCompletedRequest
    {
        public Guid BookingId { get; set; }
    }
}
