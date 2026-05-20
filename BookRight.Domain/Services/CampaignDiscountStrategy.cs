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
            return new DiscountResult(originalPrice, originalPrice * (1m - _discountPercent), DiscountType.Campaign.ToString()); //Calculate base price *
        }

                                                   
    }
}