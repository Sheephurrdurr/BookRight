namespace BookRight.Facade.DTOs.CreateTherapistDTOs
{
    public sealed record CreateTherapistRequest(
        string FirstName,
        string LastName,
        string Email,
        string Specialization,
        string AuthorizationType,
        string AuthorizationNumber,
        Guid ClinicId,
        List<Guid> TreatmentTypeIds);
}