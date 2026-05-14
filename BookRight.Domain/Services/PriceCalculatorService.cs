using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Services.Interfaces;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    public class PriceCalculatorService : IPricecalculatorService
    {
        public Money CalculateBasePrice(TreatmentType treatmentType) //Returns the base price of TreatmentType
        {
            return treatmentType.Price;
        }

        public Money ApplyAddOns(Money price, IEnumerable<AddOn> addOns) //Applies all add-ons/surcharges to the current price
        {
            Money totalAddOnAmount = addOns //Calculates total amount of all add-ons
                .Select(addOn => addOn.CalculateAmount(price))
                .Aggregate(new Money(0), (total, amount) => total + amount);

            return price + totalAddOnAmount;
        }
        
        public DiscountResult ApplyDiscount(Money basePrice, decimal percentage) //Applies discount percentage to base price
        {
            decimal discountMultiplier = percentage / 100; //Converts pertentage to multiplier, ex. 10% -> 0.10

            Money discountAmount = basePrice * discountMultiplier; //Calculates discount amount

            Money discountedPrice = basePrice - discountAmount; //Calculates final price incl. discount

            return new DiscountResult(
                basePrice,
                discountedPrice,
                $"{percentage}% rabat");
        }
    }
}