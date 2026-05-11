using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;


namespace BookRight.UseCases.Interfaces.Service
{
    public interface ILoyaltyService
    {
        
        decimal CalculateTotalPurchasesLast12Months( //Calculates total purchases from completed bookings within the last 12 months
            IEnumerable<Booking> bookings,
            DateTime currentDate);

        LoyaltyLevel GetLoyaltyLevel( //Determines customer loyalty lvl
            IEnumerable<Booking> bookings,
            DateTime currentDate
            );
    }
}