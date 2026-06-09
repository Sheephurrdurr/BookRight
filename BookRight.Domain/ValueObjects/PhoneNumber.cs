using BookRight.Domain.Exceptions;

namespace BookRight.Domain.ValueObjects
{
    public sealed record PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            // Phone number is required.
            if (string.IsNullOrWhiteSpace(value))
                throw new PhoneNumberIsRequiredException();

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
