using BookRight.Domain.Errors;

namespace BookRight.Domain.ValueObjects
{
    public sealed record TimeSlot
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration => EndTime - StartTime;

        public TimeSlot() { } // Parameterless constructor for EF Core
        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException(
                    DomainErrorMessages.EndTimeMustBeLaterThanStartTime,
                    nameof(endTime));

            StartTime = startTime;
            EndTime = endTime;
        }

        public int DurationMinutes()
        {
            return (int)Duration.TotalMinutes;
        }

        // Metoden, der tjekker for overlap mellem to TimeSlot-objekter for at sikre, at bookninger ikke overlapper hinanden.
        // Den returnerer true, hvis der er overlap, og false ellers.
        public bool OverlapsWith(TimeSlot other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return StartTime < other.EndTime && other.StartTime < EndTime; // Opverlapper, hvis starttidspunktet for det første TimeSlot er før slutttidspunktet for det andet TimeSlot,
                                                                           // og starttidspunktet for det andet TimeSlot er før sluttidspunktet for det første TimeSlot.
        }

        public bool OverlapsWith(DateTime startTime, DateTime endTime)
        {
            return StartTime < endTime && EndTime > startTime; // Opverlapper, hvis starttidspunktet for det første TimeSlot er før slutttidspunktet for det andet
        }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
}