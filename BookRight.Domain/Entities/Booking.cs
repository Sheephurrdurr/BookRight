using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSlot TimeSlot { get; private set; }
        public int Duration { get; private set; }
        public BookingStatus Status { get; private set; }

        private readonly List<BookingLine> _lines = new();
        public IReadOnlyCollection<BookingLine> Lines => _lines.AsReadOnly();

        // Foreign key og INGEN Navigation property
        public Guid CustomerId { get; private set; }

        public Guid CampaignDiscountId { get; private set; }

       // public List<TreatmentType> CombinedTreatments { get; private set; } = new(); // kan undgå null fejl på liste

        private Booking() { }
        public Booking(Guid id, Guid customerId, TimeSlot timeSlot, BookingStatus status)
        {
            Id = id;
            CustomerId=customerId;
            CustomerId = CustomerId;
            TimeSlot = timeSlot;
            Status = status;
        }

        // Creating Booking
        public static Booking CreateBooking(Guid id, Guid CustomerId, TimeSlot timeSlot, BookingStatus status)
        {
            return new Booking(id, CustomerId, timeSlot, status);    
        }

        //Edit Booking
        public void EditBookingTimeSlot(TimeSlot newTimeSlot)
        {
            TimeSlot = newTimeSlot;
        }

        // Cancelling Booking
        public void CancelBooking()
        {
            Status = BookingStatus.Cancelled;
        }

        // MarkAsNoshow
        public void MarkAsNOShow()
        {
            Status = BookingStatus.NoShow;
        }

        // ConfirmBooking
        public void ConfirmBooking()
        {
            Status = BookingStatus.Completed;
        }


    }
}

