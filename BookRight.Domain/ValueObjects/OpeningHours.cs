using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public record OpeningHours
    {
        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }

        
        private OpeningHours() { } //Til EF Core


        public OpeningHours(TimeOnly openTime, TimeOnly closeTime)
        {
            if (closeTime <= openTime)
            {
                throw new ArgumentException("Lukketidspunkt skal være efter åbningstidspunkt.");
            }

            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}

