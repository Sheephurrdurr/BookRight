using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    public interface IPriceCalculatorService
    {
        Money CalculateBasePrice(TreatmentType treatmentType);
        Money ApplyAddOns(Money price, IEnumerable<AddOn> addOns);
        DiscountResult ApplyDiscount(Money basePrice, decimal percentage, DiscountType discountType);

        // This method iterates through all registered discount strategies, calculates the discount for each strategy, 

        Task<DiscountResult> CalculateBestDiscountAsync(
        Customer customer,
        Booking booking,
        IEnumerable<Booking> completedBookings);

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
    }
}
