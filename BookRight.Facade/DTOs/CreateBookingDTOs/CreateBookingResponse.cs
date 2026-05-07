using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public class CreateBookingResponse
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeSlot { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
    }
}
