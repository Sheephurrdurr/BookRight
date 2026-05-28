namespace BookRight.Facade.DTOs.UpdateClinicDTOs
{
    public sealed record UpdateClinicRequest(
        Guid ClinicId,
        string Name,
        string Street,
        string City,
        string PostalCode,
        string Phone,
        int NumTreatmentRooms);
}