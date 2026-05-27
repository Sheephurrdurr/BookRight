
namespace BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs
{
    public record GetAllTreatmentTypesResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int DurationMinutes { get; init; }
        public int MaxParticipants { get; init; }
        public decimal Price { get; init; }
        public bool CanBeCombined { get; init; }
        public string? RequiredSpecialization { get; init; }



    }
}
