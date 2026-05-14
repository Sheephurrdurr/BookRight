namespace BookRight.Domain.ValueObjects
{
    public record TimeSlot 
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration => EndTime - StartTime;

        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            if (startTime < DateTime.Today)
                throw new ArgumentException("Start time cannot be in the past.");

            if (endTime <= startTime)
                throw new ArgumentException("End time must be later than start time");

            StartTime = startTime;
            EndTime = endTime;
        }

        public int DurationMinutes()
        {
            return (int)Duration.TotalMinutes; // Cast to int
        }

        // Overload metode tager TimeSlot og returnerer false hvis intet overlap er fundet
        public bool OverlapsWith(TimeSlot other)
        {   // yaki -ændret 
            /* return OverlapsWith(other.StartTime, other.EndTime);*/
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
