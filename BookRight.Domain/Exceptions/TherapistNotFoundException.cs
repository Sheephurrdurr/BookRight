using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class TherapistNotFoundException : DomainException
{
    public TherapistNotFoundException(Guid therapistId)
        : base(DomainErrorMessages.TherapistNotFound(therapistId))
    {
    }
}