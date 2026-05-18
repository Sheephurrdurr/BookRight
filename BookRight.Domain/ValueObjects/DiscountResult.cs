using System;

namespace BookRight.Domain.ValueObjects
{
    // public  -> Kan bruges fra andre layers/projekter.
    // sealed  -> Kan ikke nedarves.
    // record  -> Sammenlignes på værdier i stedet for reference.
    public sealed record DiscountResult : IComparable<DiscountResult>
    {
        public Money OriginalPrice { get; }
        public Money DiscountedPrice { get; }
        public string DiscountName { get; }

        public DiscountResult(
            Money originalPrice,
            Money discountedPrice,
            string discountName)
        {
            OriginalPrice = originalPrice;
            DiscountedPrice = discountedPrice;
            DiscountName = discountName;
        }

        public int CompareTo(DiscountResult? other)
        {
            if (other is null)
                return -1;

            return DiscountedPrice.Value
                .CompareTo(other.DiscountedPrice.Value);
        }
    }
}