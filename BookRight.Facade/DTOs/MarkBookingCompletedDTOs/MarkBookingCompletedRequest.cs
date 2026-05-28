namespace BookRight.Facade.DTOs.MarkBookingCompletedDTOs
{
    public sealed record MarkBookingCompletedRequest
    {
        public Guid BookingId { get; set; }
    }
}
