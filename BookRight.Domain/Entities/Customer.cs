using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Entities
{
    public record Customer
    {
        public Guid Id { get; private set; }
        public FullName Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }

        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        private Customer() { } // Kræves af EF Core

        public Customer(FullName name, Email email, PhoneNumber phone)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }
    }
}
