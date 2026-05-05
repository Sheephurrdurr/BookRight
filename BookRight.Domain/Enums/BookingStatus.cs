using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Enums
{
    public enum BookingStatus
    {
        Confirmed,//Booking er oprettet/bekræftet
        Completed,//Kunden er mødt op og behandlingen er gennemført
        Cancelled,
        NoShow
    }
}
