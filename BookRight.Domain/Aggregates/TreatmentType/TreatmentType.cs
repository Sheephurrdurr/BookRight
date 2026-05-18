using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.TreatmentType
{
    public class TreatmentType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; //Not nullable, but a promise to the constructor, that the property is set later. Fixes warning.
        public int DurationMinutes { get; private set; }
        public int MaxParticipants { get; private set; }
        public Money Price { get; private set; } = null!; //Not nullable

        private TreatmentType() { } //EF core constructor


        public TreatmentType(string name, int durationMinutes, int maxParticipants, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(name));

            if (durationMinutes <= 0)
                throw new ArgumentException(
                    DomainErrorMessages.DurationMustBeGreaterThanZero,
                    nameof(durationMinutes));

            if (maxParticipants <= 0)
                throw new ArgumentException(
                    DomainErrorMessages.MaxParticipantsMustBeGreaterThanZero,
                    nameof(maxParticipants));

            Id = Guid.NewGuid();
            Name = name;
            DurationMinutes = durationMinutes;
            MaxParticipants = maxParticipants;
            Price = price ?? throw new ArgumentNullException(nameof(price)); //Nullcheck
        }

    }
}
