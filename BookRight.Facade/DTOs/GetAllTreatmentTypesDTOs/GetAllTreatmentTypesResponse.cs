namespace BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs
{
    public record GetAllTreatmentTypesResponse(
        Guid Id,
        string Name,
        int DurationMinutes,
        decimal Price
    );
}