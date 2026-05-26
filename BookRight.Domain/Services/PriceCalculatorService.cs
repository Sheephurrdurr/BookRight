using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

using BookRight.Domain.Services.DiscountStrategies;

namespace BookRight.Domain.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        // The constructor takes an IEnumerable of IDiscountStrategy, allowing for dependency injection of all available discount strategies.
        public PriceCalculatorService(IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _discountStrategies = discountStrategies;
        }

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

        // This method determines which add-ons should be automatically applied based on the time of the booking.
        public IEnumerable<AddOn> GetAutomaticAddOns(TimeSlot timeSlot)
        {
            var addOns = new List<AddOn>();

            var isWeekend =
                timeSlot.StartTime.DayOfWeek == DayOfWeek.Saturday ||
                timeSlot.StartTime.DayOfWeek == DayOfWeek.Sunday;

            var isEvening = timeSlot.StartTime.Hour >= 17;

            if (isWeekend || isEvening)
            {
                addOns.Add(new AddOn("Aften-/weekendtillæg", 15));
            }

            return addOns;
        }

        public DiscountResult ApplyDiscount(Money basePrice, decimal percentage, DiscountType discountType) //Applies discount percentage to base price
        {
            decimal discountMultiplier = percentage / 100; //Converts pertentage to multiplier, ex. 10% -> 0.10

            Money discountAmount = basePrice * discountMultiplier; //Calculates discount amount

            Money discountedPrice = basePrice - discountAmount; //Calculates final price incl. discount

            return new DiscountResult(
                basePrice,
                discountedPrice,
                discountType);
        }

        // This method iterates through all registered discount strategies, calculates the discount for each strategy,
        // and returns the best discount result (the one with the lowest discounted price).
        public async Task<DiscountResult> CalculateBestDiscountAsync(PricingContext context)
        {
            var tasks = _discountStrategies.Select(strategy =>
                Task.Run(() => strategy.CalculateDiscount(context)));

            var results = await Task.WhenAll(tasks);

            return results.Min()!;

        }
    }
}