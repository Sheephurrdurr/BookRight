using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs
{
    // Request DTO for marking a booking as NoShow.
    public record MarkBookingAsNoShowRequest 
    {
        public Guid BookingId { get; set; }
    }
}
