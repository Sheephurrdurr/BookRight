using System;

namespace BookRight.Domain.ValueObjects
{
    public record OpeningHours
    {
        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }

        public OpeningHours(TimeOnly openTime, TimeOnly closeTime)
        {
            if (openTime >= closeTime)
                throw new ArgumentException("Åbningstid skal være før lukketid.");

            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}