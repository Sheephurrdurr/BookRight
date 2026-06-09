using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using System;

namespace BookRight.Domain.ValueObjects
{
    public sealed record FullName
    {
        public string FirstName { get; }
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) //First name is required
                throw new FirstNameIsRequiredException();

            if (string.IsNullOrWhiteSpace(lastName)) //Last name is required
                throw new LastNameIsRequiredException();

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
