using BookRight.Domain.ValueObjects;

namespace BookRight.Facade.DTOs.CreateCustomerDTOs
{
    public record BookingLineRequest
    {
        public Guid TherapistTreatmentTypeId { get; set; }

        public Money BasePrice { get; set; }
    }
}
