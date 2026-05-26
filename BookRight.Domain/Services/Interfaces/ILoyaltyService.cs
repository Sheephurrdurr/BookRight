using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services.Interfaces
{
    public interface ILoyaltyService
    {
        Money CalculateTotalPurchasesLast12Months(//Calculates total purchases from completed bookings within the last 12 months
            IEnumerable<Booking> bookings,
            DateTime currentDate);

        LoyaltyLevelType GetLoyaltyLevel(//Determines the customer's loyalty level
            IEnumerable<Booking> bookings,
            DateTime currentDate);
    }
}