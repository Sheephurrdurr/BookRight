using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException(string email)
        : base(DomainErrorMessages.EmailAlreadyExists(email))
    {
    }
}