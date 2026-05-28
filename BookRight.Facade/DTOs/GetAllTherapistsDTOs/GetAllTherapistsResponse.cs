namespace BookRight.Facade.DTOs.GetAllTherapistsDTOs
{
    public sealed record GetAllTherapistsResponse(
         Guid Id,
         string FirstName,
         string LastName,
         string Email,
         string Specialization,
         string AuthorizationType,
         string AuthorizationNumber,
         Guid ClinicId,
         string ClinicName,
        List<Guid> TreatmentTypeIds
     );
}
