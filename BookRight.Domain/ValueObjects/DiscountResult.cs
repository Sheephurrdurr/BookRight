using System;
using BookRight.Domain.Enums;

namespace BookRight.Domain.ValueObjects
{
    // public  -> Kan bruges fra andre layers/projekter.
    // sealed  -> Kan ikke nedarves.
    // record  -> Sammenlignes på værdier i stedet for reference.
    public sealed record DiscountResult : IComparable<DiscountResult>
    {
        public Money OriginalPrice { get; }
        public Money DiscountedPrice { get; }
        public DiscountType AppliedDiscount { get; }
        public decimal DiscountPercentage =>
            (1 - DiscountedPrice.Value / OriginalPrice.Value) * 100;
        public string DiscountName => AppliedDiscount.ToDisplayName(DiscountPercentage);


        public DiscountResult(
            Money originalPrice,
            Money discountedPrice,
            DiscountType appliedDiscount)
        {
            OriginalPrice = originalPrice;
            DiscountedPrice = discountedPrice;
            AppliedDiscount = appliedDiscount;
        }

        public int CompareTo(DiscountResult? other)
        {
            if (other is null)
                return 1;

            return DiscountedPrice.Value
                .CompareTo(other.DiscountedPrice.Value);
        }
    }
}