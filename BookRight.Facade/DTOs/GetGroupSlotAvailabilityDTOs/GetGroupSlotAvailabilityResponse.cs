
namespace BookRight.Facade.DTOs.GetGroupSlotAvailabilityDTOs
{
    public record GetGroupSlotAvailabilityResponse
    {
        public int MaxParticipants { get; set; }
        public int CurrentParticipants { get; set; }
        public int RemainingSlots { get; set; }
        public bool IsFullyBooked { get; set; }
    }
}
