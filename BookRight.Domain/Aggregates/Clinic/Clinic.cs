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

        private Clinic() { }

        // Constructor: bruges til at oprette en ny Clinic og give den startværdier
        public Clinic(string name, Address address, PhoneNumber phone, int numTreatmentRooms)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Klinikkens navn må ikke være tomt.");

            if (address == null)
                throw new ArgumentException("Klinikkens adresse må ikke være tom.");

            if (phone == null)
                throw new ArgumentException("Klinikkens telefonnummer må ikke være tomt.");

            if (numTreatmentRooms <= 0)
                throw new ArgumentException("Antal behandlingsrum skal være større end 0.");

            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Phone = phone;
            NumTreatmentRooms = numTreatmentRooms;
        }
    }
}