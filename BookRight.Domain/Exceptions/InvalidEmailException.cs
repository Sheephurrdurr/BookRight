using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException()
            : base(DomainErrorMessages.InvalidEmailAddress)
        { }
    }
}
