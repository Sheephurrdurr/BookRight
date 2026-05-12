using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Services.Interfaces
{
    public interface IPriceCalculator
    {
        Money CalculateBasePrice(TreatmentType treatmenttype);
        Money ApplyAddOns(Money price, IEnumerable<AddOn> addOns);
        DiscountResult ApplyDiscount(Money basePrice, decimal percentage);
    }

}
