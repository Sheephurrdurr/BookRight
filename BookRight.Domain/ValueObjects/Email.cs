using BookRight.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@')) //Email must contain a valid value
                throw new ArgumentException(
                    DomainErrorMessages.InvalidEmailAddress,
                    nameof(value));

            Value = value.ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}
