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

        public bool OverlapsWith(TimeSlot other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return StartTime < other.EndTime && other.StartTime < EndTime;
        }

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