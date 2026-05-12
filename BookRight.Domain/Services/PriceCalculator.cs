using BookRight.Domain.Aggregates;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        public Money CalculateBasePrice(TreatmentType treatmenttype)
        {
            return treatmenttype.Price;
        }
        
        public Money ApplyAddOns(Money price, IEnumerable<AddOn> addOns)
        {
            foreach (var addOn in addOns)
            {
                price += addOn.Price;
            }
            return price;
        }
        public DiscountResult ApplyDiscount(Money basePrice, decimal percentage)
        {
            var discountAmount = basePrice * percentage;
            var discountedPrice = basePrice - discountAmount;

            return new DiscountResult(basePrice, discountedPrice, "Rabat i kr. "+ (basePrice - discountedPrice));

        }
    }
}
