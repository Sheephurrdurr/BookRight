using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Entities
{
    public record Therapist
    {
        public Guid Id { get; private set; }
        public FullName Name { get; private set; }
        public Email Email { get; private set; } 
        public string Specialization { get; private set; } 

        private Therapist() { } // Kræves af EF Core

        public Therapist(FullName name, Email email, string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization is required.", nameof(specialization));

            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Specialization = specialization;
        }
    }
}
