namespace BookRight.Facade.DTOs.ValueObjectDTOs
{
    public record TimeSlotDto(DateTime StartTime, DateTime EndTime); // Positional record, short and sweet
    // DTO used to transfer booking start and end times
}
