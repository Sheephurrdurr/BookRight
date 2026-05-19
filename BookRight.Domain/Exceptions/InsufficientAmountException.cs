using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class InsufficientAmountException : DomainException
{
    public InsufficientAmountException() //Thrown when attempting to subtract more money than available
        : base(DomainErrorMessages.InsufficientAmount)
    {
    }
}