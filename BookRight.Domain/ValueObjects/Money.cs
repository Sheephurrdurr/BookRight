using System;

namespace BookRight.Domain.ValueObjects
{
    // public  -> Kan bruges fra andre layers/projekter.
    // sealed  -> Kan ikke nedarves.
    // record  -> Sammenlignes på værdier i stedet for reference.
    public sealed record Money : IComparable<Money>
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentException(
                    "Money cannot be negative.");

            Value = value;
        }

        public static Money operator +(Money left, Money right)
            => new(left.Value + right.Value);

        public static Money operator -(Money left, Money right)
        {
            if (right.Value > left.Value)
                throw new InvalidOperationException(
                    "Cannot subtract more than the current value.");

            return new Money(left.Value - right.Value);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentException(
                    "Multiplier cannot be negative.");

            return new Money(money.Value * multiplier);
        }

        public int CompareTo(Money? other)
        {
            if (other is null)
                return 1;

            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return $"{Value:0.00} kr";
        }

        public static bool operator >(Money left, Money right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(Money left, Money right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left.Value >= right.Value;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left.Value <= right.Value;
        }
    }
}
