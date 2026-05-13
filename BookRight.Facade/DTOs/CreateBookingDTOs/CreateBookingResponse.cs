using BookRight.Facade.DTOs.ValueObjectDTOs;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public class CreateBookingResponse
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ClinicId { get; set; }
        public TimeSlotDto TimeSlot { get; set; }
        public string Status { get; set; }
    }
}
