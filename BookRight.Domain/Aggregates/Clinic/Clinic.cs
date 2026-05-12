using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Clinic
{
    public record Clinic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public int NumTreatmentRooms { get; private set; }

        // Privat dictionary, som kun Clinic-klassen selv kan ændre direkte
        private Dictionary<DayOfWeek, OpeningHours> _openingHours = new();

        // Andre klasser kan kun læse åbningstiderne, ikke ændre dem direkte
        public IReadOnlyDictionary<DayOfWeek, OpeningHours> OpeningHours => _openingHours;

        // Constructor: bruges til at oprette en ny Clinic og give den startværdier
        public Clinic(string name, string street, string city, string zipCode, string phone, int numTreatmentRooms)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Klinikkens navn må ikke være tomt.");


            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Klinikkens vejnavn må ikke være tom.");

            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("Klinikkens bynavn må ikke være tomt.");

            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentException("Klinikkens postnummer må ikke være tomt.");

        
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Klinikkens telefonnummer må ikke være tomt.");

            if (numTreatmentRooms <= 0)
                throw new ArgumentException("Antal behandlingsrum skal være større end 0.");

            Id = Guid.NewGuid();
            Name = name;
            Address = new Address(street, city, zipCode);
            Phone = new PhoneNumber(phone);
            NumTreatmentRooms = numTreatmentRooms;
        }
        /*
        // Metoden til at oprette en Clinic
        public static Clinic CreateClinic(string name, string address, string phone, int numTreatmentRooms)
        {
            return new Clinic(name, address, phone, numTreatmentRooms);
        }

        // Metoden til at ændre klinikkens oplysninger
        public void EditClinic(string name, string address, string phone, int numTreatmentRooms)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Klinikkens navn må ikke være tomt.");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Klinikkens adresse må ikke være tom.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Klinikkens telefonnummer må ikke være tomt.");

            if (numTreatmentRooms <= 0)
                throw new ArgumentException("Antal behandlingsrum skal være større end 0.");

            Name = new FullName(name);
            Address = new Address(address);
            Phone = new PhoneNumber(phone);
            NumTreatmentRooms = numTreatmentRooms;
        }
        */

        // Metoden til at tilføje eller ændre åbningstider for en bestemt ugedag
        public void SetOpeningHours(DayOfWeek day, OpeningHours openingHours)
        {
            if (openingHours == null)
                throw new ArgumentNullException(nameof(openingHours));

            _openingHours[day] = openingHours;
        }
    }
}