using System;

namespace BookRight.Domain.ValueObjects
{
    public sealed class Money : IComparable<Money>
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Money cannot be negative.");

            Value = value;
        }

        public static Money operator +(Money left, Money right) //Adds two Money objects together
            => new Money(left.Value + right.Value);

        public static Money operator -(Money left, Money right) //Subtracts one Money object from another
        {
            if (right.Value > left.Value)
                throw new InvalidOperationException(
                    "Cannot subtract more than the current value.");

            return new Money(left.Value - right.Value);
        }

        public static Money operator *(Money money, decimal multiplier) //Multiplies Money by a decimal multiplier

        {
            if (multiplier < 0)
                throw new ArgumentException(
                    "Multiplier cannot be negative.");

            return new Money(money.Value * multiplier);
        }

        public int CompareTo(Money? other)//Compares Money objects
        {
            if (other is null)
                return 1;

            return Value.CompareTo(other.Value);
        }

        public override bool Equals(object? obj) //Equality comparison
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

        public static bool operator ==(Money left, Money right) //Equality operators
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Money left, Money right) //Comparison operators
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
