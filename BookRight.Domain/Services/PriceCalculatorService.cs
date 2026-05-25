using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Enums;
using BookRight.Domain.Services.Interfaces;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
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
        public async Task<DiscountResult> CalculateBestDiscountAsync(
            Customer customer,
            Booking booking,
            IEnumerable<Booking> completedBookings)
        {
            var tasks = _discountStrategies.Select(strategy =>
                Task.Run(() => strategy.CalculateDiscount(customer, booking, completedBookings)));

            var results = await Task.WhenAll(tasks);

            return results
                .OrderBy(result => result.DiscountedPrice.Value)
                .First();

        }





    }
}