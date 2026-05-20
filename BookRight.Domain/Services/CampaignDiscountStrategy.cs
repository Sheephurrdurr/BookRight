using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Services
{
    public class CampaignDiscountStrategy //Interface når den er lagt ind
    {
        private readonly decimal _discountPercent; //Private field

        public CampaignDiscountStrategy (decimal discountPercent)
        {
            _discountPercent = discountPercent;
        }

        public DiscountResult CalculateDiscount(Customer customer, Booking booking, IEnumerable<Booking> completedBookings)
        {
            var originalPrice = booking.GetTotalPrice(); //Recieve base price of booking
            return new DiscountResult(originalPrice, originalPrice * (1m - (_discountPercent/100)), DiscountType.Campaign.ToString());
            //base price * 1 - % men i decimal, 15% = 0.15, dermed Returnerer 85% af originalprisen ved 15% rabat som nypris,
                                                       //_discountPercent/100 fordi receptionist ikke skal taste 0,15, men 15%
        }


    }
}