using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    public class DoubleBookingVerificationService //Responsible for enforcing booking rules that involve multiple bookings. The logic can't belong to a single Booking entity, bcause a Booking only knows its own state
                                                  // and not other bookings that should be checked for overlaps
    {
        public void CustomerBookingVerification(IEnumerable<Booking> existingCustomerBooking, TimeSlot slot) //Verifies customer doesn't already have an active booking, that overlaps with the requested timeslot
        {
            var customerOverlap = existingCustomerBooking
                .Where(c => c.Status != BookingStatus.Cancelled) //Ignore cancelled bookings when checking availability
                .FirstOrDefault(c => slot.OverlapsWith(c.TimeSlot)); //Finds 1st booking whose timeslot overlaps with the timeslot
            if (customerOverlap != null) //If overlaps exists, prevents double booking
                throw new CustomerAlreadyHasBookingException();
        }

        public void TherapistVerification(IEnumerable<Booking> existingTherapistBooking, TimeSlot slot, int maxParticipants) //Verifies therapist not fully booked for the given timeslot
        {
            var therapistOverlap = existingTherapistBooking
                .Where(c => c.Status != BookingStatus.Cancelled) 
                .Where(c => slot.OverlapsWith(c.TimeSlot)); //Select bookings that overlaps with the timeslot
            if (therapistOverlap.Count() >= maxParticipants) //If no. of overlapped booking reaches max, no more customers (participants) can be booked.
                throw new TherapistAlreadyHasBookingException();
        }
    }
    
}
