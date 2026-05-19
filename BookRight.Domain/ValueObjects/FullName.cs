using BookRight.Domain.Errors;
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
                throw new ArgumentException(
                    DomainErrorMessages.FirstNameIsRequired,
                    nameof(firstName));
            
            if (string.IsNullOrWhiteSpace(lastName)) //Last name is required
                throw new ArgumentException(
                    DomainErrorMessages.LastNameIsRequired,
                    nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
