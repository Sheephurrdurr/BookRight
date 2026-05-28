namespace BookRight.Facade.DTOs.CreateTreatmentTypeDTOs
{
    public sealed record CreateTreatmentTypeRequest
    {
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public int MaxParticipants { get; set; }
        public decimal Price { get; set; }
        public bool CanBeCombined { get; set; }
        public string? RequiredSpecialization { get; set; }
    }
}
