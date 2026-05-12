using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Services.Interfaces;

namespace BookRight.Domain.Services
{
    public class LoyaltyService : ILoyaltyService //Responsible for calculating customer loyalty. BL related to loyalty lvls based on completed bookings within the last 12 months
    {
      
        public decimal CalculateTotalPurchasesLast12Months( //Customers total purchases from completed bookings within 12 months
            IEnumerable<Booking> bookings,
            DateTime currentDate)
        {
        
            if (bookings is null) //Guard clause ensures bookings !null
                throw new ArgumentNullException(nameof(bookings));

            var fromDate = currentDate.AddMonths(-12); //Earliest booking date (12 months back)

            return bookings //Filters bookings within valid period and sums up total price
                .Where(b =>
                    b.Status == BookingStatus.Completed &&
                    b.TimeSlot.StartTime >= fromDate &&
                    b.TimeSlot.StartTime <= currentDate)
                .Sum(b => b.GetTotalPrice());
        }

        public LoyaltyLevel GetLoyaltyLevel( //Determines the customers loyalty lvl. It's also stateless 'cause it doesn't save data. Which means it's thread safe (no race conditions) and no setup of intern state = easy tetsing.
            IEnumerable<Booking> bookings,
            DateTime CurrentDate)
        {
            var totalPurchases = //Calculates total amount
                CalculateTotalPurchasesLast12Months(bookings, CurrentDate);

            //Determine loyalty level based on business rules

            
            if (totalPurchases > 25000) //Gold: purchases above 25.000 kr.
                return LoyaltyLevel.Gold;

            
            if (totalPurchases > 10001) //Silver: purchases between 10.001 and 25.000 kr.
                return LoyaltyLevel.Silver;

            
            if (totalPurchases >= 3000) //Bronze: purchases between 3.000 and 10.000 kr.
                return LoyaltyLevel.Bronze;

            return LoyaltyLevel.None; //No loyalty lvl if purchase is <3.000 kr.
        }
    }
}