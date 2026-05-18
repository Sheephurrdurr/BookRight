
using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class TherapistIsNotAvailableException : DomainException
{
    public TherapistIsNotAvailableException()
        : base(DomainErrorMessages.TherapistIsNotAvailable)
    {
    }
}