using BookRight.Domain.Errors;

namespace BookRight.Domain.ValueObjects
{
    public sealed record TimeSlot 
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration => EndTime - StartTime;

        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            if (startTime < DateTime.Today)
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(startTime));

            if (endTime <= startTime)
                throw new ArgumentException(
                    DomainErrorMessages.EndTimeMustBeLaterThanStartTime,
                    nameof(endTime));

            StartTime = startTime;
            EndTime = endTime;
        }

        public int DurationMinutes()
        {
            return (int)Duration.TotalMinutes; // Cast to int
        }

        // Overload metode tager TimeSlot og returnerer false hvis intet overlap er fundet
        public bool OverlapsWith(TimeSlot other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return this.StartTime < other.EndTime && other.StartTime < this.EndTime;
        } 
        // Metode tager tidspunkter og returnerer false, hvis intet overlap er fundet.
        public bool OverlapsWith(DateTime startTime, DateTime endTime)
        {
            return StartTime < endTime && EndTime > startTime;
        }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
}
