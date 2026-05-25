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
        IEnumerable<AddOn> GetAutomaticAddOns(TimeSlot timeSlot);


        // This method iterates through all registered discount strategies, calculates the discount for each strategy, 

        Task<DiscountResult> CalculateBestDiscountAsync(
            Customer customer,
            Booking booking,
            IEnumerable<Booking> completedBookings);
    }
}
