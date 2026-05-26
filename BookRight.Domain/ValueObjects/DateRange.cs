using BookRight.Domain.Errors;

namespace BookRight.Domain.ValueObjects
{
    public sealed record DateRange
    {
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }

        public DateRange() { }
        public DateRange(DateOnly start, DateOnly end)
        {
            if (start >= end)
                throw new ArgumentException(
                    DomainErrorMessages.EndDateCannotBeBeforeStartDate,
                    nameof(start));

            StartDate = start;
            EndDate = end;
        }

        public bool Overlaps(DateRange other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other)); //Guard clause

            return StartDate < other.EndDate &&
                   EndDate > other.StartDate;
        }

        public bool Contains(DateOnly date)
        {
            return date >= StartDate &&
                   date < EndDate;
        }
    }
}
// public  -> Kan bruges fra andre layers/projekter.
// sealed  -> Kan ikke nedarves.
// record  -> Sammenlignes på værdier i stedet for reference.
