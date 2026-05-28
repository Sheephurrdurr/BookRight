namespace BookRight.Facade.DTOs.RestoreBookingFromNoShowDTOs
{
    // Request DTO used to restore a NoShow booking.
    public sealed record RestoreBookingFromNoShowRequest(Guid BookingId);
}
