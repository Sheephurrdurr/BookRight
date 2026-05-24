namespace BookRight.Facade.DTOs.ValueObjectDTOs
{
    public record TimeSlotDto 
    {
        public DateTime StartTime { get; init; } 
        public DateTime EndTime { get; init; }
    };
}
