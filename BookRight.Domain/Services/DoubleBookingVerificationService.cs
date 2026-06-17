using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Services
{
    // Denne serviceklasse indeholder metoder til at verificere, om en kunde eller terapeut allerede har en booking, der overlapper med det ønskede tidsrum.
    public class DoubleBookingVerificationService
    {
        public void CustomerBookingVerification(IEnumerable<Booking> existingCustomerBooking, TimeSlot slot) 
        {
            var customerOverlap = existingCustomerBooking
                .Where(c => c.Status != BookingStatus.Cancelled) // Vi ignorerer "cancelled bookings", da de ikke længere er aktive
                .FirstOrDefault(c => slot.OverlapsWith(c.TimeSlot)); 
            if (customerOverlap != null)
                throw new CustomerAlreadyHasBookingException();
        }

        public void TherapistVerification(IEnumerable<Booking> existingTherapistBooking, TimeSlot slot, int maxParticipants)
        {
            var therapistOverlap = existingTherapistBooking
                .Where(c => c.Status != BookingStatus.Cancelled)
                .Where(c => slot.OverlapsWith(c.TimeSlot));
            if (therapistOverlap.Count() >= maxParticipants)
                throw new TherapistAlreadyHasBookingException();
        }
    }
    
}
