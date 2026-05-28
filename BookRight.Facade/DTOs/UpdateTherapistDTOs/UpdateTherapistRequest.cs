namespace BookRight.Facade.DTOs.UpdateTherapistDTOs
{
    public sealed record UpdateTherapistRequest(
        Guid TherapistId,
        string FirstName,
        string LastName,
        string Email,
        string Specialization,
        string AuthorizationType,
        string AuthorizationNumber,
        Guid ClinicId,
        List<Guid> TreatmentTypeIds
    );
}