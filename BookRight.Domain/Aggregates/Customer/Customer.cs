    using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Errors;
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
    public int? BirthdayDiscount { get; private set; }
    private Customer() { }

    public Customer(FullName name, Email email, PhoneNumber phone, DateOnly dateOfBirth, string? healthNotes, Guid? preferredTherapistId)
    {
        Id = Guid.NewGuid();

        Name = name ?? throw new ArgumentNullException(nameof(name)); //Nullchecks
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));

        if (dateOfBirth == default) //ErrorMessage
            throw new ArgumentException(
                DomainErrorMessages.DateOfBirthIsRequired,
                nameof(dateOfBirth));

        DateOfBirth = dateOfBirth;
        HealthNotes = healthNotes;
        PreferredTherapistId = preferredTherapistId; 
    }

    public void UpdateHealthNotes(string? healthNotes)
    {
        HealthNotes = healthNotes;
    }

    public bool IsEligibleForBirthdayDiscount(DateOnly treatmentDate)
    {
        bool IsBirthdayMonth = treatmentDate.Month == DateOfBirth.Month;

        bool alreadyUsedDiscountThisYear = BirthdayDiscount == treatmentDate.Year;

        return IsBirthdayMonth && !alreadyUsedDiscountThisYear;
    }
} 