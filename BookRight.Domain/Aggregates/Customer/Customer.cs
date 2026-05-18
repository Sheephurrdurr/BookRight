using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Customer;
public class Customer
{
    public Guid Id { get; private set; }
    public FullName Name { get; private set; } = null!; //Not nullable. It's a promise to the constructor, that Name is set later. Fixes warning. 
    public Email Email { get; private set; } = null!; //Not nullable
    public PhoneNumber Phone { get; private set; } = null!; //Not nullable
    public DateOnly DateOfBirth { get; private set; }
    public string? HealthNotes { get; private set; } //Nullable
    public Guid? PreferredTherapistId { get; private set; } //Nullable

    private Customer() { }

    public Customer(FullName name, Email email, PhoneNumber phone, DateOnly dateOfBirth, string? healthNotes, Guid? preferredTherapistId)
    {
        Id = Guid.NewGuid();

        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));

        if (dateOfBirth == default) throw new ArgumentException("Fødselsdato skal udfyldes.", nameof(dateOfBirth));

        DateOfBirth = dateOfBirth;
        HealthNotes = healthNotes;
        PreferredTherapistId = preferredTherapistId; 
    }

    // Controlled access to sensitive data: Health notes are only modifiable through this method.
    // This ensures that any changes to health notes are intentional and go through the proper channels, maintaining data integrity and security.
    public void UpdateHealthNotes(string? healthNotes)
    {
        HealthNotes = healthNotes;
    }

    // Prevents health note property from every being exposed outside of the aggregate. Only way to update it is through this method, which is part of the aggregate's behavior.
    // GDPR compliance: Health notes are sensitive personal data, so we want to ensure that they are only accessible and modifiable through controlled methods within the aggregate.
    public override string ToString() => 
        $"Customer {{ Id={Id}, Name={Name}  }}";
    
} 