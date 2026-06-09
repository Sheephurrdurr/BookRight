using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Exceptions
{
    public class BookingOutsideOpeningHoursException : DomainException
    {
        public BookingOutsideOpeningHoursException(TimeSlot timeSlot)
        : base(DomainErrorMessages.BookingOutsideOpeningHours(timeSlot))
        {
        }
    }
}
