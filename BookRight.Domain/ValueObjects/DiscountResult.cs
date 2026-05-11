using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public sealed class DiscountResult : IComparable<DiscountResult>
    {
        public Money OriginalPrice { get; }
        public Money DiscountedPrice { get; }
        public string DiscountName { get; }

        public DiscountResult(Money originalPrice, Money discountedPrice, string discountName)
        {
            OriginalPrice = originalPrice;
            DiscountedPrice = discountedPrice;
            DiscountName = discountName;
        }

        public int CompareTo(DiscountResult? other)
        {
            if (other is null) return -1;
            return DiscountedPrice.Value.CompareTo(other.DiscountedPrice.Value);
        }
    }

}
