using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class CustomerAlreadyExistsException : DomainException
{
    public CustomerAlreadyExistsException(string email)
        : base(DomainErrorMessages.CustomerAlreadyExists(email))
    {
    }
}