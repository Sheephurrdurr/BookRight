using BookRight.Domain.Enums;

namespace BookRight.Domain.Entities
{
    public class BookingLine
    {
        public Guid Id { get; private set; }
        public Guid TherapistTreatmentTypeId { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal Surcharge { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public decimal FinalPrice { get; private set; }

        public BookingLine() { }

        public BookingLine(Guid therapistTreatmentTypeId, decimal basePrice, decimal discountPercent, decimal surcharge)
        {

        }
    }
}
