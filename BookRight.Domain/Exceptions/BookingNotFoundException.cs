using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class BookingNotFoundException : DomainException
{
    public BookingNotFoundException(Guid bookingId)
        : base(DomainErrorMessages.BookingNotFound(bookingId))
    {
    }
}