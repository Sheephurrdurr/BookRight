using BookRight.Domain.Exceptions;
using System;

namespace BookRight.Domain.ValueObjects

//Represents a monetary value in the domain. It's a VO, meaning it's compared by it's value instead memory reference.
//The Money object ensures that Money never can't be negative, invalid calculations are prevented.
//Ex. 
// Instead of using decimal directly everywhere in the system, we wrap it inside Money to protect domain logic.
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
                throw new ArgumentException(nameof(value));

            Value = value;
        }

        public static Money operator +(Money amount, Money amountToAdd)
    => new(amount.Value + amountToAdd.Value);

        public static Money operator -(Money amount, Money amountToSubtract) //Thrown when attempting to subtract more Money than available
        {
            if (amountToSubtract.Value > amount.Value)
                throw new InsufficientAmountException(); //CustomException

            return new Money(amount.Value - amountToSubtract.Value);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentException(nameof(multiplier));

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

        public static bool operator >(Money amount, Money otherAmount)
        {
            return amount.Value > otherAmount.Value;
        }

        public static bool operator <(Money amount, Money otherAmount)
        {
            return amount.Value < otherAmount.Value;
        }

        public static bool operator >=(Money amount, Money otherAmount)
        {
            return amount.Value >= otherAmount.Value;
        }

        public static bool operator <=(Money amount, Money otherAmount)
        {
            return amount.Value <= otherAmount.Value;
        }
    }
}
