using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class CustomerNotFoundException : DomainException
{
    public CustomerNotFoundException(Guid customerId)
        : base(DomainErrorMessages.CustomerNotFound(customerId))
    {
    }
}