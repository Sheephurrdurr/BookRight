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
        {
            return OverlapsWith(other.StartTime, other.EndTime);
        } 
        // Metode tager tidspunkter og returnerer false, hvis intet overlap er fundet.
        public bool OverlapsWith(DateTime startTime, DateTime endTime)
        {
            return StartTime < endTime && EndTime > startTime;
        }
        // yaki : Returnerer true hvis booking er om aftenen (efter 18:00) eller i  weekenden
        public bool IsEveningOrWeekend()
        {
          var isEvening = StartTime.Hour >= 18;
            var isWeedend = StartTime.DayOfWeek == DayOfWeek.Saturday ||
                   StartTime.DayOfWeek == DayOfWeek.Sunday;
            return isEvening || isWeedend;
        }

        // Bergner 15% tillæg  hvis aften eller weekend
        public decimal ApplySurCharge(decimal basePrice)
        {
            if (IsEveningOrWeekend())
                return basePrice * 1.15m; // 15 % tillæg
          return basePrice;
        }
        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
}
