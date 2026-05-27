
namespace BookRight.Facade.DTOs.GetBookingsByWeekDTOs
{
    public record GetByWeekResponse
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid TherapistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
