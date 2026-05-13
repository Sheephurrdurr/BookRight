using BookRight.Facade.DTOs.ValueObjectDTOs;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public class CreateBookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ClinicId { get; set; }
        public TimeSlotDto TimeSlot { get; set; }
    }
}
