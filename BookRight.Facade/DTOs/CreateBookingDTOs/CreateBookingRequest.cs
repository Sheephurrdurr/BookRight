using BookRight.Facade.DTOs.CreateCustomerDTOs;
using BookRight.Facade.DTOs.ValueObjectDTOs;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public record CreateBookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ClinicId { get; set; }
        public TimeSlotDto TimeSlot { get; set; }
        public List<BookingLineRequest> Lines { get; set; }
    }


}
