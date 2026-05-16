
namespace BookRight.Facade.DTOs.GetAllTherapistTreatmentTypesDTOs
{
    public record GetAllTherapistTreatmentTypesResponse(
        Guid Id,
        Guid TherapistId,
        string TherapistName,
        string TreatmentTypeName,
        decimal BasePrice
    );
}
