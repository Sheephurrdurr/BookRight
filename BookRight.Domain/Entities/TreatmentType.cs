using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Entities
{
    public record TreatmentType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int DurationMinutes { get; private set; }
        public int MacParticipants { get; private set; }

        public TreatmentType(string name, int durationMinutes, int maxParticipants)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Treatment type name cannot be empty.", nameof(name));
            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.", nameof(durationMinutes));
            if (maxParticipants <= 0)
                throw new ArgumentException("Max participants must be greater than zero.", nameof(maxParticipants));
            Id = Guid.NewGuid();
            Name = name;
            DurationMinutes = durationMinutes;
            MacParticipants = maxParticipants;
        }
    }
}
