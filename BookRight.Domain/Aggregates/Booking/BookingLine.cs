using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Enums;
using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Booking
{
    public class BookingLine
    {
        public Guid Id { get; private set; }
        public Guid TherapistTreatmentTypeId { get; private set; }

        public Money BasePrice { get; private set; } = null!; //BasePrice IS NOT nullable. It's a promise to the EF constructor, that BasePrice is set later. If not it results in a warning.
        public decimal DiscountPercent { get; private set; }

        public DiscountType DiscountType { get; private set; }

        public AddOn.AddOn? AddOn { get; private set; } //Aften/weekendtillæg. Nullable.

        public Guid? AddOnId { get; private set; } //Saving Id on AddOn for EF Core.

        public Money FinalPrice { get; private set; } = null!; //FinalPrice IS NOT nullable. It's a promise to the EF constructor, that FinalPrice is set later. If not it results in a warning.

        private BookingLine() { } //Empty EF Core constructor

        public BookingLine(
            Guid therapistTreatmentTypeId,
            Money basePrice,
            decimal discountPercent,
            DiscountType discountType,
            AddOn.AddOn? addOn = null)
        {
            if (therapistTreatmentTypeId == Guid.Empty)
                throw new ArgumentException(
                    nameof(therapistTreatmentTypeId));

            if (basePrice is null)
                throw new ArgumentNullException(
                    nameof(basePrice));

            if (discountPercent < 0 || discountPercent > 100)
                throw new InvalidPercentageException();


            Id = Guid.NewGuid();
            TherapistTreatmentTypeId = therapistTreatmentTypeId;
            BasePrice = basePrice;
            DiscountPercent = discountPercent;
            DiscountType = discountType;

            AddOn = addOn; //Saving AddOn if there's a Surcharge
            AddOnId = addOn?.Id;

            FinalPrice = CalculateFinalPrice(basePrice, discountPercent, addOn); //Calculates final price discount and Surcharge included.
        }

        private static Money CalculateFinalPrice( // Static since no instance state is used.
            Money basePrice,
            decimal discountPercent,
            AddOn.AddOn? addOn)
        {
            decimal discountMultiplier = 1 - (discountPercent / 100); //Discount subtraction

            Money priceAfterDiscount = basePrice * discountMultiplier;

            if (addOn is null) //If no Addon, return only price after Discount
                return priceAfterDiscount;

            Money addOnAmount = addOn.CalculateAmount(priceAfterDiscount); //AddOn calculates Surcharge. Ex. 15 % on the weekends

            return priceAfterDiscount + addOnAmount;
        }
    }
}