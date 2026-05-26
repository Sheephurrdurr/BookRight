using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class TherapistAlreadyHasBookingException : DomainException
{
    public TherapistAlreadyHasBookingException()
        : base(DomainErrorMessages.TherapistAlreadyHasBooking)
    {
    }
}
