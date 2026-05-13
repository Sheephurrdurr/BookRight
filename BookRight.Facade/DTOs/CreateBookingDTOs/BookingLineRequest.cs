using BookRight.Domain.ValueObjects;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public record BookingLineRequest
    {
        public Guid TherapistTreatmentTypeId { get; set; }

        public Money BasePrice { get; set; }
    }
}
