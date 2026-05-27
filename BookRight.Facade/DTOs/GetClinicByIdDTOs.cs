namespace BookRight.Facade.DTOs.GetClinicByIdDTOs
{
    public record GetClinicByIdResponse(
        Guid Id,
        string Name,
        string Street,
        string City,
        string PostalCode,
        string Phone,
        int NumTreatmentRooms);
}