
namespace BookRight.Facade.DTOs.GetAllTherapistTreatmentTypesDTOs
{
    public sealed record GetAllTherapistTreatmentTypesResponse(
        Guid Id,
        Guid TherapistId,
        string TherapistName,
        Guid TreatmentTypeId,
        string TreatmentTypeName,
        decimal BasePrice,
        int DurationMinutes
    );
}
