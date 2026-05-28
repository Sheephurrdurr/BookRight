namespace BookRight.Facade.DTOs.CreateClinicDTOs
{
    public sealed record CreateClinicRequest(
        string Name,
        string Street,
        string City,
        string PostalCode,
        string Phone,
        int NumTreatmentRooms,
        IReadOnlyList<CreateClinicOpeningHourRequest> OpeningHours);


    // DTO til at repræsentere en åbningstid for klinikken i UI
    public record CreateClinicOpeningHourRequest(
        DayOfWeek DayOfWeek,
        TimeOnly OpenTime,
        TimeOnly CloseTime);
}