using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public sealed class Money : IComparable<Money>
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Money må ikke være negativ.");

            Value = value;
        }

        // Addition
        public static Money operator +(Money left, Money right)
            => new Money(left.Value + right.Value);

        // Subtraction
        public static Money operator -(Money left, Money right)
        {
            if (right.Value > left.Value)
                throw new InvalidOperationException("Cannot subtract more than the current value.");

            return new Money(left.Value - right.Value);
        }

        // Multiplication with decimal
        public static Money operator *(Money money, decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentException("Multiplier cannot be negative.");

            return new Money(money.Value * multiplier);
        }


        // CompareTo
        public int CompareTo(Money? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        // Equality
        public override bool Equals(object? obj)
        {
            if (obj is Money other)
                return Value == other.Value;

            return false;
        }


        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


        public override string ToString()
        {
            return $"{Value:0.00} kr";
        }

    }


}
