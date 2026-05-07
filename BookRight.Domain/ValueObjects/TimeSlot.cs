namespace BookRight.Domain.ValueObjects
{
    public record TimeSlot 
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }

        public TimeSlot(DateTime start, DateTime end, TimeSpan duration)
        {
            if (start < DateTime.Today)
                throw new ArgumentException("Start time cannot be in the past.");

            if (end <= start)
                throw new ArgumentException("End time must be later than start time");

            StartTime = start;
            EndTime = end;
        }

        // Overload metode tager TimeSlot og returnerer false hvis intet overlap er fundet
        public bool OverlapsWith(TimeSlot other)
        {
            return OverlapsWith(other.StartTime, other.EndTime);
        } 
        // Metode tager tidspunkter og returnerer false, hvis intet overlap er fundet.
        public bool OverlapsWith(DateTime start, DateTime end)
        {
            return StartTime < end && EndTime > start;
        }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
}
