using BookRight.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public sealed record PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            // Phone number is required.
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    DomainErrorMessages.PhoneNumberCannotBeNull,
                    nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
