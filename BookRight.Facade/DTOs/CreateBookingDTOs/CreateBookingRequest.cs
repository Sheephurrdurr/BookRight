
namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public sealed record CreateBookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ClinicId { get; set; }
        public Guid TherapistId { get; set; }
        public DateTime StartTime { get; set; } = default!; //Avoid nullable warnings. TimeSlot is required, but the compiler doesn't know that TimeSlot gets set later.
        public List<BookingLineRequest> Lines { get; set; } = new(); //Initializes the list to prevent null reference exceptions
                                                                     //if no booking lines are provided in the request
        public Guid? CampaignDiscountId { get; set; }
    }


}
