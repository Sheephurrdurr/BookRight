
namespace BookRight.Facade.DTOs.GetBookingsForTodayDTOs
{
    public sealed record GetBookingsForTodayResponse(
        Guid BookingId,
        DateTime StartTime,
        Guid ClinicId,
        string CustomerName,
        string TherapistName,
        string Status);
    
}
