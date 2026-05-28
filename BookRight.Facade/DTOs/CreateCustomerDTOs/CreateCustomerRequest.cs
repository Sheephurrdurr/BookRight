namespace BookRight.Facade.DTOs.CreateCustomerDTOs
{
    public sealed record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateOnly DateOfBirth,
    string? HealthNotes,
    Guid? PreferredTherapistId);
}
