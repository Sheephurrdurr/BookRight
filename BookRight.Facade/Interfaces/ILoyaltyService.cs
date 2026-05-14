using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;


namespace BookRight.Domain.Services
{
    public interface ILoyaltyService
    {
        
        decimal CalculateTotalPurchasesLast12Months( //Calculates total purchases from completed bookings within the last 12 months
            IEnumerable<Booking> bookings,
            DateTime currentDate);

        LoyaltyLevelType GetLoyaltyLevel( //Determines customer loyalty lvl
            IEnumerable<Booking> bookings,
            DateTime currentDate
            );
    }
}