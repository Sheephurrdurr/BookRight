using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.Booking;

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

        if (dateOfBirth == default) throw new ArgumentException("Date of birth is required.", nameof(dateOfBirth));

        DateOfBirth = dateOfBirth;
        HealthNotes = healthNotes;
        PreferredTherapistId = preferredTherapistId;
    }
} 