using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions;

public class InvalidAddOnPercentageException : DomainException
{
    public InvalidAddOnPercentageException() //An exception is thrown when AddOn-percentage is outside the valid range from 0-100.
        : base(DomainErrorMessages.AddOnPercentageOutOfRange) //Sends the predefined error-message to the base DomianException class.
    {
    }
}