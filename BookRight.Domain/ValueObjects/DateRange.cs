using BookRight.Domain.Errors;
using System;

namespace BookRight.Domain.ValueObjects
{
    public sealed record DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateRange(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ArgumentException(
                    DomainErrorMessages.EndDateCannotBeBeforeStartDate,
                    nameof(start));

            Start = start;
            End = end;
        }

        public bool Overlaps(DateRange other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other)); //Guard clause
            return Start < other.End &&
                   End > other.Start;
        }

        public bool Contains(DateTime date)
        {
            return date >= Start &&
                   date < End;
        }
    }
}
// public  -> Kan bruges fra andre layers/projekter.
// sealed  -> Kan ikke nedarves.
// record  -> Sammenlignes på værdier i stedet for reference.
