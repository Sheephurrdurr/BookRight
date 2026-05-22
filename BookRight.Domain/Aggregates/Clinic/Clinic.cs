using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Clinic
{
    public class Clinic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; //Not nullable, but a promise to the constructor, that the name is set later. Fixes warning.
        public Address Address { get; private set; } = null!; //Not nullable
        public PhoneNumber Phone { get; private set; } = null!; //Not nullable
        public int NumTreatmentRooms { get; private set; }

        // Privat liste, som kun Clinic-klassen selv kan ændre direkte
        private readonly List<ClinicOpeningHour> _openingHours = new();

        // Andre klasser kan kun læse åbningstiderne, ikke ændre dem direkte
        public IReadOnlyCollection<ClinicOpeningHour> OpeningHours => _openingHours.AsReadOnly();

        private readonly List<TherapistSchedule> _therapistSchedules = new(); // --- DDD POLICE!! WEEWOOO -- Aggregates are only linked to other aggregates via Ids, not direct refererences. Change this at some point, or Kaj is gonna grill us. Alive.
        public IReadOnlyCollection<TherapistSchedule> TherapistSchedules => _therapistSchedules.AsReadOnly();

        private readonly List<Therapist> _therapists = new(); //--- DDD POLICE!! WEEWOOO -- 
        public IReadOnlyCollection<Therapist> Therapists // --DDD SWAT TEAM!! WEEWOOO -- Aggregates are only linked to other aggregates via Ids, not direct refererences. 
            => _therapists.AsReadOnly();

        private Clinic() { }

        // Constructor: bruges til at oprette en ny Clinic og give den startværdier
        public Clinic(string name, Address address, PhoneNumber phone, int numTreatmentRooms)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(name));

            if (address is null)
                throw new ArgumentNullException(
                    nameof(address),
                    DomainErrorMessages.AddressCannotBeNull);

            if (phone is null)
                throw new ArgumentNullException(
                    nameof(phone),
                    DomainErrorMessages.PhoneNumberCannotBeNull);

            if (numTreatmentRooms <= 0)
                throw new ArgumentException(
                    DomainErrorMessages.NumberOfTreatmentRoomsMustBeGreaterThanZero,
                    nameof(numTreatmentRooms));

            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Phone = phone;
            NumTreatmentRooms = numTreatmentRooms;
        }
    }
}