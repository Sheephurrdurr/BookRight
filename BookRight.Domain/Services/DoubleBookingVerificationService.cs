using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    public class DoubleBookingVerificationService
    {
        //Verifies that new booking doesn't overlap with any existing booking for specific customer
        public void CustomerBookingVerification(IEnumerable<Booking> existingCustomerBooking, TimeSlot slot) 
        {
            var customerOverlap = existingCustomerBooking //Gets an existing booking from an enumerable list
                .Where(c => c.Status != BookingStatus.Cancelled) 
                .FirstOrDefault(c => slot.OverlapsWith(c.TimeSlot)); //Finds the first booking that overlaps with the input timeslot
            if (customerOverlap != null)
                throw new CustomerAlreadyHasBookingException();
        }

        //Verifies that new booking doesn't overlap with any existing booking for specific therapist, taking room limit into account
        public void TherapistVerification(IEnumerable<Booking> existingTherapistBooking, TimeSlot slot, int maxParticipants)
        {
            var therapistOverlap = existingTherapistBooking //Gets existing booking from list
                .Where(c => c.Status != BookingStatus.Cancelled)
                .Where(c => slot.OverlapsWith(c.TimeSlot)); //Finds overlapping timeslot
            if (therapistOverlap.Count() >= maxParticipants) //Instead of null, count maxParticipants
                throw new TherapistAlreadyHasBookingException();
        }
    }
    
}
