using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Booking
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public TimeSlot TimeSlot { get; private set; }
        public BookingStatus Status { get; private set; }

        private readonly List<BookingLine> _lines = new(); // private field for storing BookingLines in a Booking. 
        public IReadOnlyCollection<BookingLine> Lines => _lines.AsReadOnly(); // property is used to access BookingLines in a Booking

        // Foreign key og INGEN Navigation property
        public Guid CustomerId { get; private set; }
        public Guid ClinicId { get; private set; }
        public Guid? CampaignDiscountId { get; private set; } 

        private Booking() { }
        public Booking(Guid id, Guid customerId, Guid clinicId, TimeSlot timeSlot)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            if (customerId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty.", nameof(customerId));

            if (clinicId == Guid.Empty)
                throw new ArgumentException("ClinicId cannot be empty.", nameof(clinicId));

            if (timeSlot is null)
                throw new ArgumentNullException(nameof(timeSlot));

            Id = id;
            CustomerId = customerId; 
            ClinicId = clinicId;
            TimeSlot = timeSlot;
            Status = BookingStatus.Confirmed;
        }

        public void AddLine(BookingLine line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _lines.Add(line);
        }
        public Money GetTotalPrice() //sum price of line(s)
        {
            return _lines
                .Select(line => line.FinalPrice)
                .Aggregate(new Money(0), (total, price) => total + price);
        }

        //Edit Booking TimeSlot
        public void EditTimeSlot(TimeSlot newTimeSlot)
        {
            TimeSlot = newTimeSlot;
        }

        // Cancelling Booking
        public void Cancel()
        {
            Status = BookingStatus.Cancelled;
        }

        public void Complete() 
        { 
            Status = BookingStatus.Completed; 
        }

        // MarkAsNoshow
        // Changes the booking status to NoShow.

        public void MarkAsNoShow()
        {
            Status = BookingStatus.NoShow;
        }

        public void ApplyCampaignDiscount(Guid campaignDiscountId)
        {
            if (campaignDiscountId == Guid.Empty)
                throw new ArgumentException(nameof(campaignDiscountId));

            CampaignDiscountId = campaignDiscountId;
        }
    }
}

