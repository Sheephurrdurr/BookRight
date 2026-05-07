using System;

namespace BookRight.Domain.ValueObjects
{
    //VO for åbningstider for en klinik. Indeholder åbningstid og lukketid.
    public record OpeningHours
    {
        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }



        // Constructoren, der bliver kørt, når man opretter nye åbningstider.
        public OpeningHours(TimeOnly openTime, TimeOnly closeTime)
        {
            if (openTime >= closeTime)
                throw new ArgumentException("Åbningstid skal være før lukketid.");

            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}