using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.TreatmentType
{
    public class TreatmentType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; //Not nullable, but a promise to the constructor, that the property is set later. Fixes warning.
        public int DurationMinutes { get; private set; }
        public int MaxParticipants { get; private set; }
        public Money Price { get; private set; } = null!; //Not nullable
        public bool CanBeCombined { get; private set; }
        public string? RequiredSpecialization { get; private set; }

        private TreatmentType() { } //EF core constructor


        public TreatmentType(
            string name,
            int durationMinutes,
            int maxParticipants,
            Money price,
            bool canBeCombined,
            string? requiredSpecialization)
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
            CanBeCombined = canBeCombined;
            RequiredSpecialization = requiredSpecialization;
        }

        public void UpdateTreatmentType(
            string newName,
            int newDurationMinutes,
            int newMaxParticipants,
            Money newPrice,
            bool newCanBeCombined,
            string? newRequiredSpecialization)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(newName));

            if (newDurationMinutes <= 0)
                throw new ArgumentException(
                    DomainErrorMessages.DurationMustBeGreaterThanZero,
                    nameof(newDurationMinutes));

            if (newMaxParticipants <= 0)
                throw new ArgumentException(
                    DomainErrorMessages.MaxParticipantsMustBeGreaterThanZero,
                    nameof(newMaxParticipants));

            Name = newName;
            DurationMinutes = newDurationMinutes;
            MaxParticipants = newMaxParticipants;
            Price = newPrice;
            CanBeCombined = newCanBeCombined;
            RequiredSpecialization = newRequiredSpecialization;
        }


        

    }
}
