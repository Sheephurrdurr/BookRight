using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates.Booking
{
    public class BookingLine
    {
        public Guid Id { get; private set; }
        public Guid TherapistTreatmentTypeId { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal SurchargePercent { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public decimal FinalPrice { get; private set; }

        private BookingLine() { } //Private EF core constructor

        public BookingLine(Guid therapistTreatmentTypeId, decimal basePrice, decimal discountPercent, decimal surcharge, DiscountType discountType)
        {
            if (therapistTreatmentTypeId == Guid.Empty)
                throw new ArgumentException(nameof(therapistTreatmentTypeId));

            if (basePrice <= 0)
                throw new ArgumentException("BasePrice must be above 0", nameof(basePrice));

            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException(nameof(discountPercent));

            Id = Guid.NewGuid();
            TherapistTreatmentTypeId = therapistTreatmentTypeId;
            BasePrice = basePrice;
            DiscountPercent = discountPercent;
            SurchargePercent = surcharge;
            DiscountType = discountType;
            FinalPrice = Math.Round(basePrice * (1 - discountPercent / 100) * (1 + surcharge / 100), 2);
        }
    }
}
