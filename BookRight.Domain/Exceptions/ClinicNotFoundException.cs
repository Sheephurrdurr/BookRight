using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class ClinicNotFoundException : DomainException
{
    public ClinicNotFoundException(Guid clinicId)
        : base(DomainErrorMessages.ClinicNotFound(clinicId))
    {
    }
}