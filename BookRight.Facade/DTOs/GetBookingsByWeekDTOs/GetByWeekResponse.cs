namespace BookRight.Facade.DTOs.GetBookingsByWeekDTOs
{
    public sealed record GetByWeekResponse(
    Guid BookingId,
    Guid CustomerId,
    Guid TherapistId,
    DateTime StartTime,
    DateTime EndTime,
    string Status,
    string TherapistName,
    string TreatmentName,
    string ClinicName,
    string CustomerName,
    string CustomerPhone,
    string CustomerEmail);
}