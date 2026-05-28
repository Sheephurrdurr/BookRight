namespace BookRight.BlazorUI.Components.Customer
{
    public record CustomerFormData(
        string FirstName,
        string LastName,
        string Email,
        string Street,
        string City,
        string PostalCode,
        string PhoneNumber,
        DateOnly DateOfBirth,
        string? HealthNotes,
        Guid? PreferredTherapistId
    );
}
