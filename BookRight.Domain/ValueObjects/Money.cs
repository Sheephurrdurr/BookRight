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
                throw new ArgumentException("Money value cannot be negative.");

            Value = value;
        }

        public Money Add(Money other) => new Money(Value + other.Value);

        public Money Subtract(Money other)
        {
            if (other.Value > Value)
                throw new InvalidOperationException("Cannot subtract more than the current value.");

            return new Money(Value - other.Value);
        }

        public int CompareTo(Money? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        public override string ToString() => $"{Value:0.00} kr";
    }

}
