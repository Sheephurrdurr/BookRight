namespace BookRight.Facade.DTOs.ValueObjectDTOs
{
    public record TimeSlotDto 
    {
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
    };
}
