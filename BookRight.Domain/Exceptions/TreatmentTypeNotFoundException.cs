using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class TreatmentTypeNotFoundException : DomainException
{
  public TreatmentTypeNotFoundException(Guid treatmentTypeId)
    : base(DomainErrorMessages.TreatmentTypeNotFound(treatmentTypeId))
    {
    }
}
