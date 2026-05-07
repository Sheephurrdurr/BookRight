using BookRight.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace BookRight.Domain.Entities
{
    public class Booking
    {
        public Guid BookingId { get; private set; } = Guid.NewGuid();
        public DateTime Date { get; private set; }
        public DateTime TimeSlot { get; private set; }
        public int Duration { get; private set; }
        public decimal PriceBefore_discount { get; private set; }
        public decimal price_after_discount { get; private set; }
        public BookingStatus Status { get; private set; }

        // Foreign key og Navigation property
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }

       // public List<TreatmentType> CombinedTreatments { get; private set; } = new(); // kan undgå null fejl på liste

        private Booking() { }
        public Booking(Guid Id, DateTime date, DateTime timeSlot, int duration, BookingStatus status)
        {
            BookingId = Id;
            Date = date;
            TimeSlot = timeSlot;
            Duration = duration;
            Status = status;
        }

        // Creating Booking
        public static Booking CreateBooking(Guid Id, DateTime date, DateTime timeSlot, int duration, BookingStatus status)
        {
            return new Booking(Guid.NewGuid(), date, timeSlot, duration, BookingStatus.Completed);
        }

        //Edit Booking
        public void EditBooking(DateTime newDate, DateTime newTimeSlot, int newDuration)
        {
            Date = newDate;
            TimeSlot = newTimeSlot;
            Duration = newDuration;
        }
        // Cancelling Booking
        public void CancelBooking()
        {
            Status = BookingStatus.Cancelled;
        }

        // MarkAsNoshow
        public void MarkAsNOShow()
        {
            Status = BookingStatus.NoShow;
        }

        // ConfirmBooking
        public void ConfirmBooking()
        {
            Status = BookingStatus.Completed;
        }


    }
}

