using BookRight.Facade.DTOs.ValueObjectDTOs;

namespace BookRight.Facade.DTOs.GetGroupSlotAvailabilityDTOs
{
    public record GetGroupSlotAvailabilityRequest
    {
        public Guid TherapistTreatmentTypeId { get; set; }
        public TimeSlotDto TimeSlot { get; set; }
    }
}
