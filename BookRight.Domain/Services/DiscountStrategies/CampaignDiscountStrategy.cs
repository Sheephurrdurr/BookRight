using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;


namespace BookRight.Domain.Services.DiscountStrategies
{
    public class CampaignDiscountStrategy : IDiscountStrategy
    {
        public DiscountResult CalculateDiscount(PricingContext context)
        {
            var originalPrice = context.Booking.GetBasePrice(); //Recieve base price of booking

            if (context.CampaignDiscount is null)
                return new DiscountResult(originalPrice, originalPrice, DiscountType.None);


            var bookingDate = DateOnly.FromDateTime(context.Booking.TimeSlot.StartTime);

            if (!context.CampaignDiscount.IsActive(bookingDate))
                return new DiscountResult(originalPrice, originalPrice, DiscountType.None);

            var discounted = originalPrice * (1m - context.CampaignDiscount.DiscountPercent / 100m); //base price * 1 - % men i decimal, 15% = 0.15, dermed Returnerer 85% af originalprisen ved 15% rabat som nypris,
                                                                                                     //_discountPercent/100 fordi receptionist ikke skal taste 0,15, men 15%
            return new DiscountResult(originalPrice, discounted, DiscountType.Campaign);

        }


    }
}