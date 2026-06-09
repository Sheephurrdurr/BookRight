using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions
{
    public class LastNameIsRequiredException : DomainException
    {
        public LastNameIsRequiredException()
            : base(DomainErrorMessages.LastNameIsRequired)
        {
        }
    }
}
