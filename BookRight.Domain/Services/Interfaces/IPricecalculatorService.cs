using BookRight.Domain.Aggregates.AddOn;
using BookRight.Domain.Aggregates;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Enums;

namespace BookRight.Domain.Services
{
    public interface IPriceCalculatorService
    {
        Money CalculateBasePrice(TreatmentType treatmenttype);
        Money ApplyAddOns(Money price, IEnumerable<AddOn> addOns);
        DiscountResult ApplyDiscount(Money basePrice, decimal percentage, DiscountType discountType);
    }

}
