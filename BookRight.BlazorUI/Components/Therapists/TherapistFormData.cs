namespace BookRight.BlazorUI.Components.Therapists
{
    // positional record here, fight me. I'm not doing object initializer syntax for all that. Idc I hate it.
    public record TherapistFormData(
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
