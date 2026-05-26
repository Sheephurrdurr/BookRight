namespace BookRight.Facade.DTOs.UpdateTreatmentTypeDTOs
{
    public record UpdateTreatmentTypeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public int MaxParticipants { get; set; }
        public decimal Price { get; set; } 
        public bool CanBeCombined { get; set; }
        public string? RequiredSpecialization { get; set; }

    }
}
