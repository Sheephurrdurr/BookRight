using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions
{
    public class CityCannotBeEmptyException : DomainException
    {
        public CityCannotBeEmptyException()
            : base(DomainErrorMessages.CityCannotBeEmpty)
        { }
        
    }
}
