using BookRight.Facade.DTOs.ValueObjectDTOs;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public record CreateBookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ClinicId { get; set; }
        public TimeSlotDto TimeSlot { get; set; } = default!; //Avoid nullable warnings. TimeSlot is required, but the compiler doesn't know that TimeSlot gets set later.
        public List<BookingLineRequest> Lines { get; set; } = new(); //Initializes the list to prevent null reference exceptions
                                                                     //if no booking lines are provided in the request
    }


}
