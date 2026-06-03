namespace BookRight.Facade.DTOs.GetCustomerByIdDTOs
{
    public sealed record GetCustomerByIdResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Street,
        string City,
        string PostalCode,
        string Phone,
        DateOnly DateOfBirth,
        Guid? PreferredTherapistId
    );
}
