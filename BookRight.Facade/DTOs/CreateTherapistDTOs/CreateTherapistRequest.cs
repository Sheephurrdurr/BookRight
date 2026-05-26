namespace BookRight.Facade.DTOs.CreateTherapistDTOs
{
    public record CreateTherapistRequest(
        string FirstName,
        string LastName,
        string Email,
        string Specialization,
        string AuthorizationType,
        string AuthorizationNumber,
        List<Guid> TreatmentTypeIds,
        Guid ClinicId);
}