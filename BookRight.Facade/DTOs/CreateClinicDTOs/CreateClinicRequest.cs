namespace BookRight.Facade.DTOs.CreateClinicDTOs
{
    public record CreateClinicRequest(
        string Name,
        string Street,
        string City,
        string PostalCode,
        string Phone,
        int NumTreatmentRooms);
}