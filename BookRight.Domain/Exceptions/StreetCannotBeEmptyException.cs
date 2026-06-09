using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions
{
    public class StreetCannotBeEmptyException : DomainException
    {
        public StreetCannotBeEmptyException()
            :base(DomainErrorMessages.StreetCannotBeEmpty)
        { }
    }
}
