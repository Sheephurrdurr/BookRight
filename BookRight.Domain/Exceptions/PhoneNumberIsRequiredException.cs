using BookRight.Domain.Errors;

namespace BookRight.Domain.Exceptions
{
    public class PhoneNumberIsRequiredException : DomainException
    {
        public PhoneNumberIsRequiredException()
            :base(DomainErrorMessages.PhoneNumberCannotBeNull)
        { }
    }
}
