using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Exceptions
{
    public class BookingOutsideOpeningHoursException : DomainException
    {
        public BookingOutsideOpeningHoursException(Guid clinicId, TimeSlot timeSlot)
        : base(DomainErrorMessages.BookingOutsideOpeningHours(clinicId, timeSlot))
        {
        }
    }
}
