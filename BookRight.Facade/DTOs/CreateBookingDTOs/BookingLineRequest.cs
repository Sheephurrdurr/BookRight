namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public record BookingLineRequest
    {
        public Guid TherapistTreatmentTypeId { get; set; } //Selected treatment type for the booking line
        public decimal BasePrice { get; set; } //Base price before discounts. No MOney VO here. Mapping to Money happens in CreateBookingUseCase
    }
}